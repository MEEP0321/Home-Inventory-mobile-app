using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using HomeInventory.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class ItemsViewModel: BaseViewModel
    {
        public ItemsViewModel(DbService service): base(service)
        {

            Items = new ObservableCollection<Item>();
        }

        public async Task InitializeAsync()
        {
            var itemList = await service.GetAllItems();
            Items.Clear();
            itemList.ForEach(i => Items.Add(i));
        }

        public ObservableCollection<Item> Items { get; set; }



        //Navigáció
        [RelayCommand]
        public async Task OpenItemDetailPage(Item item)
        {
            if (item is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "itemId", item.Id}
                };

                await Shell.Current.GoToAsync($"{nameof(ItemDetailsPage)}", param);
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            while (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.Navigation.RemovePage(Shell.Current.Navigation.NavigationStack[1]);
            }

            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
