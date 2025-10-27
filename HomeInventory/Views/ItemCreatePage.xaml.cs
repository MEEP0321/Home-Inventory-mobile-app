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
	}

    private async void ItemCreatePage_OnLoaded(object? sender, EventArgs e)
    {
        await vm.InitializeAsync();
    }
}