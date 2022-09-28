using Android.App.AppSearch;
using Javax.Security.Auth;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database.Models;
using PayForXatu.MAUIApp.Resources;
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
        private ICommand _removeAccountTapCommand;
        private string _email;
        private string _newPassword;
        private string _confirmNewPassword;
        private string _emailErrorMessageText;
        private bool _emailErrorMessageIsVisible;
        private string _passErrorMessageText;
        private bool _passErrorMessageIsVisible;

        public SettingsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService, IUserSettingsService userSettingsService)
            : base(navigationService, memoryCache, currencyService)
        {
            _userSettingsService = userSettingsService;
            CurrenciesList = new List<Currency>();
            Title = "Settings";
            Task getCurrenciesListTask = GetCurreciesList();
            _saveChangesCommand = new Command(async () => await SaveChangesAsync());
            _removeAccountTapCommand = new Command(OpenDeleteModal);
            EmailErrorMessageIsVisible = false;
            EmailErrorMessageText = String.Empty;
            PassErrorMessageIsVisible = false;
            PassErrorMessageText = String.Empty;
        }

        private async Task<bool> ChangeCurrencyAsync()
        {
            if (CurrentUser.UserSettings.Currency.Id != SelectedCurrency.Id)
            {
                CurrentUser.UserSettings.Currency = SelectedCurrency;
                await _userSettingsService.UpdateUserSettingsAsync(CurrentUser.UserSettings);
                SetCurrentUser(CurrentUser);
                return true;
            }
            return false;
        }

        public Action<string> OpenModal { get; set; }
        public Action<Action> OpenDeleteAccountModal { get; set; }

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

        public string EmailErrorMessageText
        {
            get { return _emailErrorMessageText; }
            set { SetProperty(ref _emailErrorMessageText, value); }
        }

        public bool EmailErrorMessageIsVisible
        {
            get { return _emailErrorMessageIsVisible; }
            set { SetProperty(ref _emailErrorMessageIsVisible, value); }
        }

        public string PassErrorMessageText
        {
            get { return _passErrorMessageText; }
            set { SetProperty(ref _passErrorMessageText, value); }
        }

        public bool PassErrorMessageIsVisible
        {
            get { return _passErrorMessageIsVisible; }
            set { SetProperty(ref _passErrorMessageIsVisible, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }
        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set { SetProperty(ref _confirmNewPassword, value); }
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

        public ICommand RemoveAccountTapCommand
        {
            get { return _removeAccountTapCommand; }
        }

        private async Task SaveChangesAsync()
        {
            EmailErrorMessageIsVisible = false;
            EmailErrorMessageText = String.Empty;
            PassErrorMessageIsVisible = false;
            PassErrorMessageText = String.Empty;

            var changeEmailResult = await _userSettingsService.ChangeEmail(CurrentUser.UserId, Email);
            if (!changeEmailResult.IsSuccess)
            {
                EmailErrorMessageIsVisible = true;
                switch (changeEmailResult.Status)
                {

                    case ChangeEmailResponseStatusEnum.IncorrectEmail:
                        EmailErrorMessageText = AppRes.IncorrectEmail;
                        break;

                    case ChangeEmailResponseStatusEnum.UserAlreadyExists:
                        EmailErrorMessageText = AppRes.UserAlreadyExists;
                        break;

                    case ChangeEmailResponseStatusEnum.Exception:
                        EmailErrorMessageText = changeEmailResult.Message;
                        break;
                }
            }


            var chPassDTO = new ChangePasswordDTO()
            {
                UserId = CurrentUser.UserId,
                ConfirmNewPassword = ConfirmNewPassword,
                NewPassword = NewPassword
            };

            var changePasswordResult = await _userSettingsService.ChangePassword(chPassDTO);
            if (!changePasswordResult.IsSuccess)
            {
                PassErrorMessageIsVisible = true;
                switch (changePasswordResult.Status)
                {
                    case ChangePasswordStatusEnum.PasswordsDoNotMatch:
                        PassErrorMessageText = AppRes.PasswordsDoNotMatch;
                        break;

                    case ChangePasswordStatusEnum.Exception:
                        PassErrorMessageText = changePasswordResult.Message;
                        break;
                }
            }

            var currencyChanged = await ChangeCurrencyAsync();

            if (changePasswordResult.Status==ChangePasswordStatusEnum.FieldsAreNotFilledIn &&
                changeEmailResult.Status==ChangeEmailResponseStatusEnum.FieldIsNotFilledIn &&
                !currencyChanged)
            {
                return;
            }

            if (changePasswordResult.IsSuccess || changeEmailResult.IsSuccess || currencyChanged)
            {

                string message = string.Empty;

                if (changeEmailResult.IsSuccess &&
                    changeEmailResult.Status!=ChangeEmailResponseStatusEnum.FieldIsNotFilledIn)
                {
                    Email = string.Empty;
                    message = AppRes.EmailWasChanged + "\n";
                }

                if (changePasswordResult.IsSuccess &&
                    changePasswordResult.Status!=ChangePasswordStatusEnum.FieldsAreNotFilledIn)
                {
                    NewPassword = string.Empty;
                    ConfirmNewPassword = string.Empty;
                    message += AppRes.PasswordWasChanged + "\n";
                }

                if (currencyChanged)
                {
                    message += AppRes.CurrencyWasChanged;
                }

                OpenModal.Invoke(message);
            }

        }

        private void OpenDeleteModal()
        {
            OpenDeleteAccountModal.Invoke(async () => await DeleteAccountAsync());
        }

        private async Task DeleteAccountAsync()
        {
            await _userSettingsService.DeleteAccountAsync(CurrentUser.UserId);
            SetCurrentUser(null);
            await _navigationService.NavigateAsync("NavigationPage/LoginPage");
        }
    }
}
