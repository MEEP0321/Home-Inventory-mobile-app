using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StoragesPage : ContentPage
{
    StoragesViewModel vm;

    public StoragesPage(StoragesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.vm = vm;

	}
    private async void StoragesPage_OnLoaded(object? sender, EventArgs e)
    {
        await vm.InitializeAsync();
    }
}