using HomeInventory.ViewModels;
namespace HomeInventory.Views;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(ItemsViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}
}