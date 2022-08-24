using PayForXatu.MobilApp.Views;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PayForXatu.MobilApp.ViewModels
{
    public class ForgotPasswordViewModel:ViewModelBase
    {
        ICommand _backButtonTapCommand;
        ICommand _resetPasswordButtonTapCommand;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;

        public ForgotPasswordViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _backButtonTapCommand = new Command(async () => await BackButtonTappedAsync());
            _resetPasswordButtonTapCommand = new Command(ResetPasswordButtonTapped);
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
            await _navigationService.NavigateAsync("LoginPage", null, null, false);
        }

        private void ResetPasswordButtonTapped()
        {
            OpenModal.Invoke();
        }

        public async Task GoToLoginPageAsync()
        {
            await _navigationService.NavigateAsync("LoginPage", null, null, false);
        }
    }
}
