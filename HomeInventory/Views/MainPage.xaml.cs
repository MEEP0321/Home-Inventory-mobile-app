using CommunityToolkit.Mvvm.Input;
using HomeInventory.ViewModels;
using HomeInventory.Views;

namespace HomeInventory
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
