using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;
using TrackEase.Models;
using TrackEase.Services;
using TrackEase.Services.Interface;

namespace TrackEase
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
        
            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddMudServices();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<GlobalState>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
