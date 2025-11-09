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
    [QueryProperty(nameof(ItemId), "itemId")]
    [QueryProperty(nameof(SourcePage), "sourcePage")]
    public partial class ItemDetailsViewModel: BaseViewModel
    {
        public ItemDetailsViewModel(DbService service): base(service)
        {

        }

        public async Task InitializeAsync()
        {
            Item = await service.GetItem(ItemId);
        }

        [ObservableProperty]
        string sourcePage;

        [ObservableProperty]
        int itemId;

        [ObservableProperty]
        Item item;

        [RelayCommand]
        public async Task Delete()
        {
            await service.DeleteItem(ItemId);
            GoBack();
        }

        //Navigáció

        [RelayCommand]
        public async Task OpenItemEditPage()
        {
            if (Item is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "itemId", Item.Id}
                };

                await Shell.Current.GoToAsync($"{nameof(ItemEditPage)}", param);
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            if (SourcePage == "View")
            {
                await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true);
            }

            if (SourcePage == "StorageDetail")
            {
                await Shell.Current.GoToAsync($"..", true);
            }
        }
  
    }
}
