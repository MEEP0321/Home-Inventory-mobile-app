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
    [QueryProperty(nameof(ItemId), "itemId")]
    public partial class ItemEditViewModel: BaseViewModel
    {

        public ItemEditViewModel(DbService service): base(service)
        {
            Storages = new ObservableCollection<Storage>();
            selectedStorage = new Storage();
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetAllStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));

            EditItem = await service.GetItem(itemId);

            IsStorageSelectionVisible = false;
            if (EditItem.ParenId != -1) 
            {
                SelectedStorageText = EditItem.Storage.Name;
            }
            else
            {
                SelectedStorageText = "-";
            }
            IsDeleteButtonVisible = false;
            FilterText = string.Empty;
        }

        [ObservableProperty]
        int itemId;

        [ObservableProperty]
        Item editItem;


        //Keresésért felel
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredBaseModels))]
        string filterText = "init";

        //Betölti az összes basemodelt, itt csak S
        ObservableCollection<Storage> Storages;

        public List<Storage> FilteredBaseModels => FilterText.Length == 0 ? Storages.ToList() : Storages.Where(w => w.Name.ToLower().Contains(FilterText.ToLower())).ToList();


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsStorageSelectionNotVisible))]
        bool isStorageSelectionVisible;

        public bool IsStorageSelectionNotVisible => !IsStorageSelectionVisible;

        [ObservableProperty]
        Storage selectedStorage;

        [ObservableProperty]
        string selectedStorageText = "init";

        [ObservableProperty]
        bool isDeleteButtonVisible;

        [RelayCommand]
        public async Task Edit()
        {
            var result = await service.UpdateItem(editItem);
            if (result is null)
            {
                await Shell.Current.DisplayAlert("Error", service.StatusMessage, "OK");
            }
            await Shell.Current.GoToAsync($"..", true);
        }

        [RelayCommand]
        public void StorageEdit()
        {
            if (EditItem.ParenId != -1)
            {
                IsDeleteButtonVisible = true;
            }
            IsStorageSelectionVisible = true;
        }

        [RelayCommand]
        public void RemoveSelection()
        {
            IsDeleteButtonVisible = false;
            SelectedStorageText = $"-";
            EditItem.ParenId = -1;
            EditItem.Location = string.Empty;
        }

        [RelayCommand]
        public void SelectStorage()
        {
            if (SelectedStorage is not null)
            {
                FilterText = SelectedStorage.Name;
                IsDeleteButtonVisible = true;
                SelectedStorageText = $"{SelectedStorage.Name}";
                EditItem.ParenId = SelectedStorage.Id;
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
