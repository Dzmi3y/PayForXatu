using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PayForXatu.MobilApp.ViewModels
{
    public class SignUpViewModel:ViewModelBase
    {
        ICommand _backButtonTapCommand;
        ICommand _okButtonTapCommand;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;
        string _password;
        string _confirmPassword;

        public SignUpViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _backButtonTapCommand = new Command(async () => await OnBackButtonTappedAsync());
            _okButtonTapCommand = new Command(OnOkButtonTappedAsync);
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

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public ICommand BackButtonTapCommand
        {
            get { return _backButtonTapCommand; }
        }

        public ICommand OkButtonTapCommand
        {
            get { return _okButtonTapCommand; }
        }

        public Action OpenModal { get; set; }

        private async Task OnBackButtonTappedAsync()
        {
            await _navigationService.NavigateAsync("LoginPage", null, null, false);
        }

        private void OnOkButtonTappedAsync()
        {
            OpenModal.Invoke();
        }

        public async Task GoToLoginPageAsync()
        {
            await _navigationService.NavigateAsync("LoginPage", null, null, false);
        }
    }
}
