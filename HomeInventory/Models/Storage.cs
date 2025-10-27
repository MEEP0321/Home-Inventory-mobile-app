using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public class Storage: IBaseModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id {  get; set; }

        public int ParentStorageId { get; set; }
        [Ignore]
        public Storage parentStorage { get; set; }

        public string Name { get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;

        [Ignore]
        public List<IBaseModel> Items { get; set; }
        public string Notes { get; set; } = string.Empty;


    }
}
