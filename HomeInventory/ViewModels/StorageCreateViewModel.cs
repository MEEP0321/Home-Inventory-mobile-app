using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
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
        private MediaService mediaService;
        public StorageCreateViewModel(DbService service) : base(service) 
        {
            this.mediaService = mediaService;
            storage = new Storage();
            storage.Type = "Tároló";
            Storages = new ObservableCollection<Storage>();
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetAllStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));
            FilterText = string.Empty;
            IsStorageSelectionVisible = false;
            IsSelectedStorageVisible = false;

            Storage.ImgPath = Path.Combine(FileSystem.AppDataDirectory, "Defaults", "StorageDefaultPic.png");
        }

        [ObservableProperty]
        bool isStorageSelectionVisible;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredBaseModels))]
        string filterText = "init";

        //Betölti az összes basemodelt, itt csak 
        ObservableCollection<Storage> Storages;

        public List<Storage> FilteredBaseModels => FilterText.Length == 0 ? Storages.ToList() : Storages.Where(w => w.Name.ToLower().Contains(FilterText.ToLower())).ToList();

        [ObservableProperty]
        Storage selectedStorage;

        [ObservableProperty]
        bool isSelectedStorageVisible;

        [RelayCommand]
        public void SelectStorage()
        {
            if (selectedStorage is not null)
            {
                FilterText = selectedStorage.Name;
                IsSelectedStorageVisible = true;
                storage.ParenId = selectedStorage.Id;
            }
        }

        [ObservableProperty]
        Storage storage;


        [RelayCommand]
        public async Task CreateStorage()
        {
            if (Storage.Name is not null && Storage.Name.Replace(" ", "").Length != 0)
            {
                var result = await service.CreateStorage(storage);

                if (result is not null)
                {
                    GoBack();
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(new AlertMessage(service.StatusMessage));
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("A név mezőt kötelező kitölteni!"));
            }
        }

        [RelayCommand]
        public void RemoveSelection()
        {
            FilterText = string.Empty;
            IsSelectedStorageVisible = false;
            storage.ParenId = -1;
        }

        partial void OnIsStorageSelectionVisibleChanging(bool value)
        {
            if (!IsStorageSelectionVisible)
            {
                RemoveSelection();
            }
        }

        [RelayCommand]
        public async Task TakePhoto()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo is not null)
            {
                var imgPath = await mediaService.SavePhotoAsync(photo);
                if (imgPath is not null)
                {
                    Storage.ImgPath = imgPath;
                }
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
