using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class ItemDetailsPage : ContentPage
{
    ItemDetailsViewModel vm;
	public ItemDetailsPage(ItemDetailsViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
	}
    private async void ItemDetailsPage_OnLoaded(object? sender, EventArgs e)
    {
        vm.InitializeAsync();
    }
}