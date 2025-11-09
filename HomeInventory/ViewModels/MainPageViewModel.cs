using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
using HomeInventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInventory.ViewModels
{
    public partial class MainPageViewModel
    {

        [RelayCommand]
        public async Task GoToItemCreatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemCreatePage)}", true);
        }

        [RelayCommand]
        public async Task GoToItemsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}", true);
        }


        [RelayCommand]
        public async Task GoToStorageCreatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(StorageCreatePage)}", true);
        }

        [RelayCommand]
        public async Task GoToStoragesPage()
        {
            await Shell.Current.GoToAsync($"{nameof(StoragesPage)}", true);
        }

        [RelayCommand]
        public async Task SendDatabaseViaEmail()
        {
            var dbPath = Constants.DatabasePath;

            if (!File.Exists(dbPath))
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("Az adatbázis nem található!"));
                return;
            }

            var message = new EmailMessage
            {
                Subject = "HomeInventory adatbázis export",
                Body = "Csatoltam az adatbázisfájlt.",
            };

            var attachment = new EmailAttachment(dbPath);
            message.Attachments.Add(attachment);

            await Email.ComposeAsync(message);
        }
    }
}
