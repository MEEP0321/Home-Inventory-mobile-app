using CommunityToolkit.Mvvm.Input;
using HomeInventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class ItemsViewModel: BaseViewModel
    {
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }

        [RelayCommand]
        public async Task GoToItemEditPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemEditPage)}", true);
        }
    }
}
