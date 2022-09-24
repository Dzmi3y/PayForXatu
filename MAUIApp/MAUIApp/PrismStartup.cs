using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PayForXatu.BusinessLogic;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database;
using PayForXatu.MAUIApp.ViewModels;
using PayForXatu.MAUIApp.Views;
using Prism;
using Prism.DryIoc;
using PayForXatu.MAUIApp.Platforms.Android;

namespace PayForXatu.MAUIApp
{
    internal static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                     //.OnAppStart("NavigationPage/LoginPage");
                     .OnAppStart("NavigationPage/SettingsPage");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<HistoryPage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<AnalyticsPage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<SettingsPage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<LoginPage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<SignUpPage>()
                         .RegisterInstance(SemanticScreenReader.Default);
            containerRegistry.RegisterForNavigation<ForgotPasswordPage>()
                         .RegisterInstance(SemanticScreenReader.Default);

            containerRegistry.RegisterScoped<ISignUpService, SignUpService>();
            containerRegistry.RegisterScoped<IGoogleManager, GoogleManager>();
            containerRegistry.RegisterScoped<ILogInService, LogInService>();
            containerRegistry.RegisterScoped<IForgotPasswordService, ForgotPasswordService>();

            containerRegistry.RegisterSingleton<IMemoryCache>(_ => new MemoryCache(new MemoryCacheOptions()));
            containerRegistry.RegisterSingleton<IFirebaseRepository,FirebaseRepository>();

        }
    }
}
