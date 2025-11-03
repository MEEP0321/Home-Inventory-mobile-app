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
    public partial class ItemEditViewModel: BaseViewModel
    {

        public ItemEditViewModel(DbService service): base(service)
        {

        }

        public async Task InitializeAsync()
        {
            EditItem = await service.GetItem(itemId);
        }

        [ObservableProperty]
        int itemId;

        [ObservableProperty]
        Item editItem;

        [RelayCommand]
        public async Task Edit()
        {
            var result = await service.UpdateItem(editItem);
            if (result is null)
            {
                await Shell.Current.DisplayAlert("Error", service.StatusMessage, "OK");
            }
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
