using HomeInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Services
{
    public class ItemService : BaseService
    {
        
        public ItemService(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Item> Create(Item item)
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

        public async Task<List<Item>> GetAll()
        {
            try
            {
                return await dbContext.Database.Table<Item>().ToListAsync();
            }
            catch (Exception ex) 
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;

        }
        public async Task<List<Storage>> GetStorages()
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

        public async Task<Item> Get(int id)
        {
            try
            {
                Item item = await dbContext.Database.Table<Item>().FirstOrDefaultAsync(x => x.Id == id);

                if (item is not null)
                {
                    item.Storage = await dbContext.Database.Table<Storage>().FirstOrDefaultAsync(s => s.Id == item.StorageId);
                    return item;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null!;
        }

        public async Task Update(int id, Item item)
        {
            try
            {
                Item itemToUpdate = await Get(id);

                if (itemToUpdate is not null)
                {
                    foreach (var prop in typeof(Item).GetProperties())
                    {
                        typeof(Item)?.GetProperty(prop.ToString()!)?.SetValue(itemToUpdate, typeof(Item)?.GetProperty(prop.ToString()!)?.GetValue(item));
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
                Item itemToDelete = await Get(id);
                await dbContext.Database.DeleteAsync(itemToDelete);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete data. {ex.Message}";
            }

        }

    }
}
