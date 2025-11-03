using HomeInventory.Services;
using HomeInventory.ViewModels;
using HomeInventory.Views;
using Microsoft.Extensions.Logging;

namespace HomeInventory
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });




            builder.Services.AddTransient<StoragesViewModel>();
            builder.Services.AddTransient<StoragesPage>();
            builder.Services.AddTransient<StorageEditViewModel>();
            builder.Services.AddTransient<StorageEditPage>();
            builder.Services.AddTransient<StorageCreateViewModel>();
            builder.Services.AddTransient<StorageCreatePage>();
            builder.Services.AddTransient<StorageDetailsViewModel>();
            builder.Services.AddTransient<StorageDetailsPage>();

            builder.Services.AddTransient<ItemsViewModel>();
            builder.Services.AddTransient<ItemsPage>();
            builder.Services.AddTransient<ItemEditViewModel>();
            builder.Services.AddTransient<ItemEditPage>();
            builder.Services.AddTransient<ItemCreateViewModel>();
            builder.Services.AddTransient<ItemCreatePage>();
            builder.Services.AddTransient<ItemDetailsViewModel>();
            builder.Services.AddTransient<ItemDetailsPage>();

            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddSingleton<DbService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
