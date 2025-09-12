using Core.Ports.Driven;
using Core.Ports.Driving;
using Core.Services;
using MauiClient.Adapters.Navigation;
using MauiClient.ViewModels;
using Microsoft.Extensions.Logging;
using Persistence.Repository.Generic;

namespace MauiClient
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

            builder.Services.AddTransient(typeof(IGenericRepo<>), typeof(SqliteGenericRepo<>));
            builder.Services.AddTransient(typeof(IGenericEntityService<>), typeof(GenericEntityService<>));
            builder.Services.AddTransient<INavigationService, ShellNavigationService>();

            builder.Services.AddSingleton<CarsListViewModel>();
            builder.Services.AddSingleton<NewCarViewModel>();
            builder.Services.AddSingleton<CarViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
