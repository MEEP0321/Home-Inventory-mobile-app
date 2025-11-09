using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class ItemCreateViewModel: BaseViewModel
    {
        public ItemCreateViewModel(DbService service): base(service)
        {
            item = new Item();
            item.Type = "Tárgy";
            selectedStorage = new Storage();
            Storages = new ObservableCollection<Storage>();
            
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetAllStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));
            FilterText = string.Empty;
            IsSelectedStorageVisible = false;
        }

        //Keresésért felel
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredBaseModels))]
        string filterText = "init";

        //Betölti az összes basemodelt, itt csak S
        ObservableCollection<Storage> Storages;

        public List<Storage> FilteredBaseModels => FilterText.Length == 0 ? Storages.ToList() : Storages.Where(w => w.Name.ToLower().Contains(FilterText.ToLower())).ToList();

        [ObservableProperty]
        Storage selectedStorage;

        [ObservableProperty]
        bool isSelectedStorageVisible;

        [RelayCommand]
        public void SelectStorage()
        {
            if (SelectedStorage is not null)
            {
                FilterText = SelectedStorage.Name;
                IsSelectedStorageVisible = true;
                Item.ParenId = SelectedStorage.Id;
            }
        }

        [RelayCommand]
        public void RemoveSelection()
        {
            FilterText = string.Empty;
            IsSelectedStorageVisible = false;
            item.ParenId = -1;
        }


        [ObservableProperty]
        Item item;

        [RelayCommand]
        public async Task CreateItem()
        {
            var result = await service.CreateItem(item);

            if (result is not null)
            {
                GoBack();
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
            while (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.Navigation.RemovePage(Shell.Current.Navigation.NavigationStack[1]);
            }

            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
