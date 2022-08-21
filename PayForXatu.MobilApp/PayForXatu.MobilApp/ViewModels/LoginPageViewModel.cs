using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PayForXatu.MobilApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        ICommand _signInTapCommand;
        ICommand _googleSignInTapCommand;
        ICommand _signUpTapCommand;
        ICommand _forgotPasswordTapCommand;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private async Task OnGoogleSignInTappedAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private async Task OnSignUpTappedAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private async Task OnForgotPasswordTappedAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
