using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class ItemEditPage : ContentPage
{
    ItemEditViewModel vm;
    public ItemEditPage(ItemEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }

    private async void ItemEditPage_OnLoaded(object? sender, EventArgs e)
    {
        vm.InitializeAsync();
    }
}