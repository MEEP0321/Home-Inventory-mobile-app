using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
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
        private MediaService mediaService;
        public ItemEditViewModel(DbService service): base(service)
        {
            this.mediaService = mediaService;
            Storages = new ObservableCollection<Storage>();
            selectedStorage = new Storage();
        }

        public async Task InitializeAsync()
        {
            var storageList = await service.GetAllStorages();
            Storages.Clear();
            storageList.ForEach(s => Storages.Add(s));
            FilterText = string.Empty;
            IsStorageSelectionVisible = false;
            EditItem = await service.GetItem(ItemId);

            if (EditItem.ParenId != -1)
            {
                SelectedStorage = EditItem.Storage;
                SelectedStorage.Location = await service.GetLocation(SelectedStorage);
            }
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
        bool isSelectedStorageVisible;

        [RelayCommand]
        public async Task Edit()
        {
            if (EditItem.Name is not null && EditItem.Name.Replace(" ", "").Length != 0)
            {
                var result = await service.UpdateItem(EditItem);
                if (result is null)
                {
                    WeakReferenceMessenger.Default.Send(new AlertMessage(service.StatusMessage));
                }
                await Shell.Current.GoToAsync($"..", true);
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("A név mezőt kötelező kitölteni!"));
            }
        }

        [RelayCommand]
        public void StorageEdit()
        {
            if (EditItem.ParenId != -1)
            {
                IsSelectedStorageVisible = true;
            }
            IsStorageSelectionVisible = true;
        }

        [RelayCommand]
        public void RemoveSelection()
        {
            FilterText = string.Empty;
            IsSelectedStorageVisible = false;
            EditItem.ParenId = -1;
            EditItem.Location = string.Empty;
        }

        [RelayCommand]
        public void SelectStorage()
        {
            if (SelectedStorage is not null)
            {
                FilterText = SelectedStorage.Name;
                IsSelectedStorageVisible = true;
                EditItem.ParenId = SelectedStorage.Id;
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
                    EditItem.ImgPath = imgPath;
                }
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
