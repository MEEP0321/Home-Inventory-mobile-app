using CommunityToolkit.Mvvm.Messaging;
using HomeInventory.Messages;
using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StorageDetailsPage : ContentPage
{
    StorageDetailsViewModel vm;
	public StorageDetailsPage(StorageDetailsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        this.vm = vm;

        WeakReferenceMessenger.Default.Register<ConfirmMessage>(this, OnConfirmMessageReceived);
    }
    private async void StorageDetailsPage_OnLoaded(object? sender, EventArgs e)
    {
        vm.InitializeAsync();
    }
    private async void OnConfirmMessageReceived(object recipient, ConfirmMessage message)
    {
        bool result = await DisplayAlert(message.Title, message.Question, "Igen", "Nem");
        message.Tcs.SetResult(result);
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}