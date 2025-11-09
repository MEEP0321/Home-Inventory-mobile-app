using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class ItemCreatePage : ContentPage
{
    ItemCreateViewModel vm;
    public ItemCreatePage(ItemCreateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.vm = vm;

        WeakReferenceMessenger.Default.Register<AlertMessage>(this, OnAlertMessageReceived);
    }

    private async void ItemCreatePage_OnLoaded(object? sender, EventArgs e)
    {
        await vm.InitializeAsync();
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