using CommunityToolkit.Mvvm.ComponentModel;
using HomeInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        protected DbService service;
        public BaseViewModel(DbService service)
        {
            this.service = service;
        }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        string statusMessage;
    }
}
