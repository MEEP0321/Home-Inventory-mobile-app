using CommunityToolkit.Mvvm.Input;
using HomeInventory.ViewModels;
using HomeInventory.Views;

namespace HomeInventory
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
