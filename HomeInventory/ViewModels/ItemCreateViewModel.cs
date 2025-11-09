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
using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;

namespace HomeInventory.ViewModels
{
    public partial class ItemCreateViewModel: BaseViewModel
    {
        private MediaService mediaService;
        public ItemCreateViewModel(DbService service, MediaService mediaService): base(service)
        {
            this.mediaService = mediaService;
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

            Item.ImgPath = Path.Combine(FileSystem.AppDataDirectory, "Defaults", "ItemDefaultPic.png");
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
            Item.ParenId = -1;
        }


        [ObservableProperty]
        Item item;

        [RelayCommand]
        public async Task CreateItem()
        {
            if (Item.Name is not null && Item.Name.Replace(" ", "").Length != 0)
            {
                var result = await service.CreateItem(Item);

                if (result is not null)
                {
                    await GoBack();
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
        public async Task TakePhoto()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo is not null)
            {
                var imgPath = await mediaService.SavePhotoAsync(photo);
                if (imgPath is not null)
                {
                    Item.ImgPath = imgPath;
                }
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
