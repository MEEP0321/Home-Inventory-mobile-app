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
    [QueryProperty(nameof(SourcePage), "sourcePage")]
    public partial class StorageDetailsViewModel : BaseViewModel
    {
        public StorageDetailsViewModel(DbService service) : base(service)
        {
        }

        public async Task InitializeAsync()
        {
            Storage = await service.GetStorage(StorageId);
        }

        [ObservableProperty]
        string sourcePage;

        [ObservableProperty]
        int storageId;

        [ObservableProperty]
        Storage storage;


        [RelayCommand]
        public async Task Delete()
        {
            await service.DeleteStorage(StorageId);
            GoBack();
        }

        [RelayCommand]
        public async Task OpenStorageEditPage()
        {
            if (Storage is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "storageId", Storage.Id}
                };

                await Shell.Current.GoToAsync($"{nameof(StorageEditPage)}", param);
            }
        }

        [RelayCommand]
        public async Task OpenBaseModel(BaseModel baseModel)
        {
            if (baseModel is not null)
            {
                if (baseModel.Type == "Tárgy")
                {
                    var param = new ShellNavigationQueryParameters
                    {
                        { "itemId", baseModel.Id},
                        { "sourcePage", "StorageDetail"}
                    };

                    await Shell.Current.GoToAsync($"{nameof(ItemDetailsPage)}", param);
                }

                if (baseModel.Type == "Tároló")
                {
                    var param = new ShellNavigationQueryParameters
                    {
                        { "storageId", baseModel.Id},
                        { "sourcePage", "StorageDetail"}
                    };

                    await Shell.Current.GoToAsync($"{nameof(StorageDetailsPage)}", param);
                }
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            if (SourcePage == "View")
            {
                await Shell.Current.GoToAsync($"{nameof(StoragesPage)}", true);
            }

            if (SourcePage == "StorageDetail")
            {
                await Shell.Current.GoToAsync($"..", true);
            }
        }
    }
}
