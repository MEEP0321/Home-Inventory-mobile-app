using HomeInventory.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Services
{
    public class DbService
    {
        protected DbContext dbContext;

        public string StatusMessage;
        public DbService(DbContext dbContext)
        {
            this.dbContext = dbContext;
            Init(this.dbContext);

        }

        public async Task Init(DbContext dbContext)
        {
            if (dbContext.Database is not null)
                return;

            dbContext.Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            var migrationResult = await dbContext.Database.CreateTablesAsync(CreateFlags.None
                , typeof(Item)
                , typeof(Storage));


        }


        //Item CRUD
        public async Task<Item> CreateItem(Item item)
        {
            try
            {
                await dbContext.Database.InsertAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to insert data. {ex.Message}";
            }
            return null;

        }

        public async Task<List<Item>> GetAllItems()
        {
            try
            {
                var itemList = await dbContext.Database.Table<Item>().ToListAsync();
                foreach (var item in itemList)
                {
                    if (item.ParenId != -1)
                    {
                        Storage storage = await GetStorage(item.ParenId);
                        item.Location = await GetLocation(storage);
                    }
                }
                return itemList;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;

        }

        public async Task<Item> GetItem(int id)
        {
            try
            {
                Item item = await dbContext.Database.Table<Item>().FirstOrDefaultAsync(x => x.Id == id);

                if (item is not null)
                {
                    if (item.ParenId != -1)
                    {
                        item.Storage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(s => s.Id == item.ParenId);
                        item.Location = await GetLocation(item.Storage);
                    }
                    return item;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;
        }

        public async Task<Item> UpdateItem(Item item)
        {
            try
            {
                await dbContext.Database.UpdateAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update data. {ex.Message}";
            }
            return null;
        }

        public async Task DeleteItem(int id)
        {
            try
            {
                Item itemToDelete = await GetItem(id);
                await dbContext.Database.DeleteAsync(itemToDelete);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete data. {ex.Message}";
            }

        }


        //Storage CRUD
        public async Task<Storage> CreateStorage(Storage storage)
        {
            try
            {
                await dbContext.Database.InsertAsync(storage);
                return storage;

            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to insert data. {ex.Message}";
            }
            return null;

        }

        public async Task<List<Storage>> GetAllStorages()
        {
            try
            {
                var storageList = await dbContext.Database.Table<Storage>().ToListAsync();

                foreach (var storage in storageList) {
                    storage.Location = await GetLocation(storage);
                }
                return storageList;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;

        }

        public async Task<Storage> GetStorage(int id)
        {
            try
            {
                Storage storage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(x => x.Id == id);

                if (storage is not null)
                {
                    List<BaseModel> models = new List<BaseModel>();
                    List<Item> items = await dbContext.Database.Table<Item>().Where(i => i.ParenId == id).ToListAsync();
                    List<Storage> storages = await dbContext.Database.Table<Storage>().Where(s => s.ParenId == id).ToListAsync();

                    models.AddRange(items.Select(i => (BaseModel)i));
                    models.AddRange(storages.Select(s => (BaseModel)s));
                    models = models.OrderByDescending(i => i.Type).ToList();

                    if (storage.ParenId != -1)
                    {
                        storage.ParentStorage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(s => s.Id == storage.ParenId);

                    }

                    storage.Items = models;

                    storage.Location = await GetLocation(storage);

                    return storage;

                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;
        }

        public async Task<Storage> UpdateStorage(Storage storage)
        {
            try
            {
                await dbContext.Database.UpdateAsync(storage);
                return storage;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update data. {ex.Message}";
            }
            return null;
        }

        public async Task DeleteStorage(int id)
        {
            try
            {
                Storage storageToDelete = await GetStorage(id);

                if (storageToDelete is not null)
                {
                    List<BaseModel> models = new List<BaseModel>();
                    List<Item> items = await dbContext.Database.Table<Item>().Where(i => i.ParenId == id).ToListAsync();
                    List<Storage> storages = await dbContext.Database.Table<Storage>().Where(s => s.ParenId == id).ToListAsync();

                    foreach (Storage storage in storages) 
                    {
                        storage.ParenId = -1;
                        UpdateStorage(storage);
                    }

                    foreach (Item item in items) 
                    { 
                        item.ParenId = -1;
                        UpdateItem(item);
                    }

      
                }

                await dbContext.Database.DeleteAsync(storageToDelete);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete data. {ex.Message}";
            }

        }

        //Others
        public async Task<string> GetLocation(Storage storage)
        {
            string location = storage.Name;
            
            while (storage is not null && storage.ParenId != -1) 
            { 
                storage = await GetStorage(storage.ParenId);
                if (storage is not null)
                {
                    location += $"/{storage.Name}";
                }
            }

            return location;
        }



    }
}
