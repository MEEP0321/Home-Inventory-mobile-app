using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class StorageCreateViewModel : BaseViewModel
    {
        StorageService service;
        public StorageCreateViewModel(StorageService service)
        {
            this.service = service;
            storage = new Storage();
        }

        [ObservableProperty]
        Storage storage;


        [RelayCommand]
        public async Task CreateStorage()
        {
            var result = await service.Create(storage);

            if (result is not null)
            {
                await Shell.Current.DisplayAlert("Siker", "Jó", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", service.StatusMessage, "OK");
            }
        }

        //Navigation
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
