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
	}
    private async void StorageDetailsPage_OnLoaded(object? sender, EventArgs e)
    {
        vm.InitializeAsync();
    }
}