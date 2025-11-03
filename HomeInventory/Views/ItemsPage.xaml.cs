using HomeInventory.ViewModels;
namespace HomeInventory.Views;

public partial class ItemsPage : ContentPage
{
    ItemsViewModel vm;
	public ItemsPage(ItemsViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
        this.vm = vm;
	}

    private async void ItemsPage_OnLoaded(object? sender, EventArgs e)
    {
        await vm.InitializeAsync();
    }
}