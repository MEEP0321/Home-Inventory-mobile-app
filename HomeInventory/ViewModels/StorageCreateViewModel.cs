using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeInventory.Models;
using HomeInventory.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class StorageCreateViewModel : BaseViewModel
    {
        public StorageCreateViewModel(DbService service) : base(service) 
        {
            storage = new Storage();
            storage.Type = "Storage";
            Storages = new ObservableCollection<Storage>();
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetAllStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));
            FilterText = string.Empty;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredBaseModels))]
        string filterText = "init";

        //Betölti az összes basemodelt, itt csak 
        ObservableCollection<Storage> Storages;

        public List<Storage> FilteredBaseModels => FilterText.Length == 0 ? Storages.ToList() : Storages.Where(w => w.Name.Contains(FilterText)).ToList();

        [ObservableProperty]
        Storage selectedStorage;

        [RelayCommand]
        public void SelectStorage()
        {
            if (selectedStorage is not null)
            {
                FilterText = selectedStorage.Name;
                storage.ParenId = selectedStorage.Id;
            }
        }

        [ObservableProperty]
        Storage storage;


        [RelayCommand]
        public async Task CreateStorage()
        {
            var result = await service.CreateStorage(storage);

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
