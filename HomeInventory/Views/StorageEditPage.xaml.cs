using HomeInventory.ViewModels;

namespace HomeInventory.Views;

public partial class StorageEditPage : ContentPage
{
	public StorageEditPage(StorageEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}