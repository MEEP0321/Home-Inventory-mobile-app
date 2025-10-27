using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public class Item: IBaseModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name {  get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;

        /*
        public bool IsInStorage { get; set; }
        public string Location { get; set; } = string.Empty;
        */
        public int StorageId { get; set; }
        [Ignore]
        public Storage Storage { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
