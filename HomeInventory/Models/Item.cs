using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public partial class Item: ObservableObject, IBaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name {  get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;

        /*
        public bool IsInStorage { get; set; }
        public string Location { get; set; } = string.Empty;
        */
        public string StorageId { get; set; } = string.Empty;
        public Storage Storage { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
