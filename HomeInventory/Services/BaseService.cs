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
    public class BaseService
    {
        protected DbContext dbContext;

        public string StatusMessage;
        public BaseService(DbContext dbContext)
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

    }
}
