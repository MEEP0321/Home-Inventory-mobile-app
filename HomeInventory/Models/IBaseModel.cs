using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public interface IBaseModel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
    }
}
