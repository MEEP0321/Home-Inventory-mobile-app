using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class ItemCreateViewModel : BaseViewModel
    {
        ItemService service;
        public ItemCreateViewModel(ItemService service)
        {
            this.service = service; 
            item = new Item();
            storages = new List<Storage>();
            
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));
        }

        [ObservableProperty]
        Item item;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredStorages))]
        string filterText = string.Empty;

        [ObservableProperty]
        List<Storage> storages;
        public List<Storage> FilteredStorages => FilterText.Length == 0 ? Storages : Storages.Where(w => w.Name.Contains(FilterText)).ToList();

        [RelayCommand]
        public async Task CreateItem()
        {
            var result = await service.Create(item);

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
