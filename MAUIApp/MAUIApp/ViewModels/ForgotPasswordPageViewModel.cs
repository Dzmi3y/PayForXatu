using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Resources;
using System.Windows.Input;


namespace PayForXatu.MAUIApp.ViewModels
{
    public class ForgotPasswordPageViewModel : ViewModelBase
    {
        ICommand _backButtonTapCommand;
        ICommand _resetPasswordButtonTapCommand;
        IForgotPasswordService _forgotPasswordService;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;

        public ForgotPasswordPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            IForgotPasswordService forgotPasswordService)
            : base(navigationService, memoryCache)
        {
            _forgotPasswordService = forgotPasswordService;
            _backButtonTapCommand = new Command(async () => await BackButtonTappedAsync());
            _resetPasswordButtonTapCommand = new Command(async () => await ResetPasswordButtonTappedAsync());
        }

        public bool ErrorMessageIsVisible
        {
            get { return _errorMessageIsVisible; }
            set { SetProperty(ref _errorMessageIsVisible, value); }
        }

        public string ErrorMessageText
        {
            get { return _errorMessageText; }
            set { SetProperty(ref _errorMessageText, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public ICommand BackButtonTapCommand
        {
            get { return _backButtonTapCommand; }
        }

        public ICommand ResetPasswordButtonTapCommand
        {
            get { return _resetPasswordButtonTapCommand; }
        }

        public Action OpenModal { get; set; }

        private async Task BackButtonTappedAsync()
        {
            await _navigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        private async Task ResetPasswordButtonTappedAsync()
        {
            ErrorMessageIsVisible = false;
            var forgotPasswordDTO = await _forgotPasswordService.ForgotPasswordAsync(Email);

            if (forgotPasswordDTO.IsSuccess)
            {
                OpenModal.Invoke();
            }
            else
            {
                ErrorMessageIsVisible = true;
                switch (forgotPasswordDTO.Status)
                {
                    case ForgotPasswordStatusEnum.EmailIsEmpty:
                        ErrorMessageText = AppRes.EmailIsEmpty;
                        break;

                    case ForgotPasswordStatusEnum.IncorrectEmail:
                        ErrorMessageText = AppRes.IncorrectEmail;
                        break;

                    default:
                        ErrorMessageText = AppRes.IncorrectEmail;
                        break;
                }
            }
        }

        public async Task GoToLoginPageAsync()
        {
            await _navigationService.NavigateAsync("NavigationPage/LoginPage");
        }
    }
}
