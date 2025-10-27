using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class ItemEditPage : ContentPage
{
    public ItemEditPage(ItemEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}