using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.Models
{
    public partial class BaseModel: ObservableObject
    {
        [ObservableProperty]
        [property: PrimaryKey]
        [property: AutoIncrement]
        int id;

        [ObservableProperty]
        int parenId = -1;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string imgPath;

        [ObservableProperty]
        string notes;

        [ObservableProperty]
        string location;

        [ObservableProperty]
        string type;
    }
}
