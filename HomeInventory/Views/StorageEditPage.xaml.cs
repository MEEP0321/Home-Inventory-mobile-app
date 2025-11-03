using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StorageEditPage : ContentPage
{
    StorageEditViewModel vm;

    public StorageEditPage(StorageEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.vm = vm;
	}

    private async void StorageEditPage_OnLoaded(object? sender, EventArgs e)
    {
        vm.InitializeAsync();
    }
}