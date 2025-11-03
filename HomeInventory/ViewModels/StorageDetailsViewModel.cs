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
    public partial class StorageDetailsViewModel : BaseViewModel
    {
        public StorageDetailsViewModel(DbService service) : base(service)
        {
        }

        public async Task InitializeAsync()
        {
            Storage = await service.GetStorage(storageId);
        }

        [ObservableProperty]
        int storageId;

        [ObservableProperty]
        Storage storage;


        [RelayCommand]
        public async Task Delete()
        {
            await service.DeleteStorage(storageId);
            await Shell.Current.GoToAsync($"..", true);
        }

        [RelayCommand]
        public async Task OpenItemEditPage()
        {
            if (storage is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "storageId", storage.Id}
                };

                await Shell.Current.GoToAsync($"{nameof(StorageEditPage)}", param);
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(StoragesPage)}", true);
        }
    }
}
