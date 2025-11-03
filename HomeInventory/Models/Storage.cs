using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public partial class Storage: BaseModel
    {

        [ObservableProperty]
        [property:Ignore]
        Storage parentStorage;

        [ObservableProperty]
        [property: Ignore]
        List<BaseModel> items;
    }
}
