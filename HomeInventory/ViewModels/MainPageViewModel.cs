using CommunityToolkit.Mvvm.Input;
using HomeInventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class MainPageViewModel: BaseViewModel
    {

        [RelayCommand]
        public async Task GoToItemCreatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemCreatePage)}", true);
        }

        [RelayCommand]
        public async Task GoToItemsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true);
        }


        [RelayCommand]
        public async Task GoToStorageCreatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(StorageCreatePage)}", true);
        }

        [RelayCommand]
        public async Task GoToStoragesPage()
        {
            await Shell.Current.GoToAsync($"{nameof(StoragesPage)}", true);
        }
    }
}
