using Android.App.AppSearch;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database.Models;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private List<Currency> _currenciesList;
        private Currency _selectedCurrency;
        private IUserSettingsService _userSettingsService;
        private ICommand _saveChangesCommand;
        public SettingsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService, IUserSettingsService userSettingsService)
            : base(navigationService, memoryCache, currencyService)
        {
            _userSettingsService = userSettingsService;
            CurrenciesList = new List<Currency>();
            Title = "Settings";
            Task getCurrenciesListTask = GetCurreciesList();
            _saveChangesCommand = new Command(async () => await SaveChangesAsync());
        }

        private async Task ChangeCurrencyAsync()
        {
            if (CurrentUser.UserSettings.Currency.Id != SelectedCurrency.Id)
            {
                CurrentUser.UserSettings.Currency = SelectedCurrency;
                await _userSettingsService.UpdateUserSettingsAsync(CurrentUser.UserSettings);
                SetCurrentUser(CurrentUser);
            }
        }

        public async Task GetCurreciesList()
        {
            CurrenciesList = await _currencyService.GetCurrencyListAsync();
            SelectedCurrency = CurrenciesList.
                FirstOrDefault(x => x.Id == CurrentUser.UserSettings.Currency.Id);
        }

        public List<Currency> CurrenciesList
        {
            get { return _currenciesList; }
            set { SetProperty(ref _currenciesList, value); }
        }

        public Currency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set { SetProperty(ref _selectedCurrency, value); }
        }

        public ICommand SaveChangesCommand
        {
            get { return _saveChangesCommand; }
        }

        private async Task SaveChangesAsync()
        {
            await ChangeCurrencyAsync();
        }
    }
}
