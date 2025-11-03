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
    public partial class ItemDetailsViewModel: BaseViewModel
    {
        public ItemDetailsViewModel(DbService service): base(service)
        {

        }

        public async Task InitializeAsync()
        {
            Item = await service.GetItem(itemId);
        }


        [ObservableProperty]
        int itemId;

        [ObservableProperty]
        Item item;

        [RelayCommand]
        public async Task Delete()
        {
            await service.DeleteItem(itemId);
            await Shell.Current.GoToAsync($"..", true);
        }

        //Navigáció

        [RelayCommand]
        public async Task OpenItemEditPage()
        {
            if (item is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "itemId", item.Id}
                };

                await Shell.Current.GoToAsync($"{nameof(ItemEditPage)}", param);
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true);
        }
    }
}
