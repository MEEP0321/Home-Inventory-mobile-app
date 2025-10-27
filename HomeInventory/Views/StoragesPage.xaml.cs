using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StoragesPage : ContentPage
{
	public StoragesPage(StoragesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}
}