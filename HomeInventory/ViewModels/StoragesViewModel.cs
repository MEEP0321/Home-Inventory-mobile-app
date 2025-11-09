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
    public partial class StoragesViewModel : BaseViewModel
    {
        public StoragesViewModel(DbService service) : base(service)
        {
            Storages = new ObservableCollection<Storage>();
        }
        public async Task InitializeAsync()
        {
            var itemList = await service.GetAllStorages();
            Storages.Clear();
            itemList.ForEach(i => Storages.Add(i));
            FilterText = string.Empty;
        }

        public ObservableCollection<Storage> Storages { get; set; }


        //Keresésért felel
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredBaseModels))]
        string filterText = "init";

        public List<Storage> FilteredBaseModels => FilterText.Length == 0 ? Storages.ToList() : Storages.Where(w => w.Name.ToLower().Contains(FilterText.ToLower())).ToList();

        //Navigáció
        [RelayCommand]
        public async Task OpenStorageDetailPage(Storage storage)
        {
            if (storage is not null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    { "storageId", storage.Id},
                    { "sourcePage", "View"}
                };

                await Shell.Current.GoToAsync($"{nameof(StorageDetailsPage)}", param);
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
