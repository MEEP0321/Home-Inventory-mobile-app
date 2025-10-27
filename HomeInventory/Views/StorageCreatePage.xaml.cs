using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StorageCreatePage : ContentPage
{
	public StorageCreatePage(StorageCreateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}