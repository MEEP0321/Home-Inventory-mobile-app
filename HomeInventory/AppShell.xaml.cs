using HomeInventory.Views;


namespace HomeInventory
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Item
            Routing.RegisterRoute(nameof(ItemCreatePage), typeof(ItemCreatePage));
            Routing.RegisterRoute(nameof(ItemEditPage), typeof(ItemEditPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ItemDetailsPage), typeof(ItemDetailsPage));

            //Storage
            Routing.RegisterRoute(nameof(StorageCreatePage), typeof(StorageCreatePage));
            Routing.RegisterRoute(nameof(StorageEditPage), typeof(StorageEditPage));
            Routing.RegisterRoute(nameof(StoragesPage), typeof(StoragesPage));
            Routing.RegisterRoute(nameof(StorageDetailsPage), typeof(StorageDetailsPage));

        }
    }
}
