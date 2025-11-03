using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StorageCreatePage : ContentPage
{
    StorageCreateViewModel vm;
    public StorageCreatePage(StorageCreateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.vm = vm;
	}

    private async void StorageCreatePage_OnLoaded(object? sender, EventArgs e)
    {
        await vm.InitializeAsync();
    }
}