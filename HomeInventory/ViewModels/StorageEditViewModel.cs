using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using HomeInventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    [QueryProperty(nameof(StorageId), "storageId")]
    public partial class StorageEditViewModel : BaseViewModel
    {
        public StorageEditViewModel(DbService service) : base(service)
        {

        }

        public async Task InitializeAsync()
        {
            EditStorage = await service.GetStorage(storageId);
        }

        [ObservableProperty]
        int storageId;

        [ObservableProperty]
        Storage editStorage;

        [RelayCommand]
        public async Task Edit()
        {
            var result = await service.UpdateStorage(editStorage);
            if (result is null)
            {
                await Shell.Current.DisplayAlert("Error", service.StatusMessage, "OK");
            }
            await Shell.Current.GoToAsync($"{nameof(StoragesPage)}", true);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
