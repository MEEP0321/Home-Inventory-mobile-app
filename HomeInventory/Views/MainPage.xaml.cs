using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
using HomeInventory.ViewModels;
using HomeInventory.Views;

namespace HomeInventory
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            WeakReferenceMessenger.Default.Register<AlertMessage>(this, OnAlertMessageReceived);
        }

        private async void OnAlertMessageReceived(object recipient, AlertMessage message)
        {
            await DisplayAlert("Hiba", message.Value, "OK");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

    }
}
