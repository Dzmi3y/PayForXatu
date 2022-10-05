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
using PayForXatu.Database.Models;

namespace PayForXatu.MAUIApp
{
    internal static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                    // .OnAppStart("NavigationPage/LoginPage");
                     .OnAppStart("NavigationPage/HomePage");
        }

        public static async Task FirebaseInitAsync(IConfiguration Configuration)
        {
            try
            {
                IFirebaseRepository firebaseRepository = new FirebaseRepository(Configuration);

                await firebaseRepository.AddAsync(new Currency() { Id = Guid.NewGuid(), Name = "BYN" });
                await firebaseRepository.AddAsync(new Currency() { Id = Guid.NewGuid(), Name = "USD" });
                await firebaseRepository.AddAsync(new Currency() { Id = Guid.NewGuid(), Name = "EUR" });
                await firebaseRepository.AddAsync(new Currency() { Id = Guid.NewGuid(), Name = "RUB" });
            }
            catch (Exception ex)
            {

            }
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
            containerRegistry.RegisterScoped<ICurrencyService, CurrencyService>();
            containerRegistry.RegisterScoped<IUserSettingsService, UserSettingsService>();


            containerRegistry.RegisterSingleton<IMemoryCache>(_ => new MemoryCache(new MemoryCacheOptions()));
            containerRegistry.RegisterSingleton<IFirebaseRepository,FirebaseRepository>();

        }
    }
}
