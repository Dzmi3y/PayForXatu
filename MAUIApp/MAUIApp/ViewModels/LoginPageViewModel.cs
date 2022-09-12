using Microsoft.Extensions.Caching.Memory;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace PayForXatu.MAUIApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        ICommand _signInTapCommand;
        ICommand _googleSignInTapCommand;
        ICommand _signUpTapCommand;
        ICommand _forgotPasswordTapCommand;
        IMemoryCache _memoryCache;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;
        string _password;

        public LoginPageViewModel(INavigationService navigationService, IMemoryCache memoryCache)
            : base(navigationService)
        {
            _memoryCache = memoryCache;
            _signInTapCommand = new Command(async ()=>await OnSignInTappedAsync());
            _googleSignInTapCommand = new Command(async () => await OnGoogleSignInTappedAsync());
            _signUpTapCommand = new Command(async () => await OnSignUpTappedAsync());
            _forgotPasswordTapCommand = new Command(async () => await OnForgotPasswordTappedAsync());
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

        public ICommand SignInTapCommand
        {
            get { return _signInTapCommand; }
        }

        public ICommand GoogleSignInTapCommand
        {
            get { return _googleSignInTapCommand; }
        }

        public ICommand SignUpTapCommand
        {
            get { return _signUpTapCommand; }
        }

        public ICommand ForgotPasswordTapCommand
        {
            get { return _forgotPasswordTapCommand; }
        }

        private async Task OnSignInTappedAsync()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        private async Task OnGoogleSignInTappedAsync()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        private async Task OnSignUpTappedAsync()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }

        private async Task OnForgotPasswordTappedAsync()
        {
            await _navigationService.NavigateAsync("ForgotPasswordPage");
        }
    }
}
