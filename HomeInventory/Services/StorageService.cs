using HomeInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Services
{
    public class StorageService: BaseService
    {
        public StorageService(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Storage> Create(Storage storage)
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

        public async Task<List<Storage>> GetAll()
        {
            try
            {
                return await dbContext.Database.Table<Storage>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;

        }

        public async Task<Storage> Get(int id)
        {
            try
            {
                Storage storage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(x => x.Id == id);

                if (storage is not null)
                {
                    List<IBaseModel> models = new List<IBaseModel>();
                    List<Item> items = await dbContext.Database.Table<Item>().Where(i => i.StorageId == id).ToListAsync();
                    List<Storage> storages = await dbContext.Database.Table<Storage>().Where(s => s.ParentStorageId == id).ToListAsync();

                    models.AddRange(items.Select(i => (IBaseModel)i));
                    models.AddRange(storages.Select(s => (IBaseModel)s));

                    storage.parentStorage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(s => s.Id == storage.ParentStorageId);

                    storage.Items = models;

                    return storage;

                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;
        }

        public async Task Update(int id, Storage storage)
        {
            try
            {
                Storage storageToUpdate = await Get(id);

                if (storageToUpdate is not null)
                {
                    foreach (var prop in typeof(Storage).GetProperties())
                    {
                        typeof(Storage)?.GetProperty(prop.ToString()!)?.SetValue(storageToUpdate, typeof(Storage)?.GetProperty(prop.ToString()!)?.GetValue(storage));
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update data. {ex.Message}";
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Storage storageToDelete = await Get(id);
                await dbContext.Database.DeleteAsync(storageToDelete);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete data. {ex.Message}";
            }

        }
    }
}
