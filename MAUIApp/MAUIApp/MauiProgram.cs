using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.MAUIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder()
           .UseMauiApp<App>()
           .UseMauiCommunityToolkit()
           .UsePrism(prism => PrismStartup.Configure(prism))
           .ConfigureFonts(fonts =>
           {
               fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
               fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               fonts.AddFont("chicago_regular.ttf", "ChicagoRegular");
               fonts.AddFont("chiller_regular.ttf", "ChillerRegular");
               fonts.AddFont("digital7_mono.ttf", "Digital7Mono");
           });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("PayForXatu.MAUIApp.appsettings.json");
            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
           // Task t = PrismStartup.FirebaseInitAsync(config);
            builder.Configuration.AddConfiguration(config);

            return builder.Build();
        }
    }
}
