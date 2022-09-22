using Microsoft.Extensions.Caching.Memory;
using PayForXatu.Database;
using System.Windows.Input;
using PayForXatu.BusinessLogic;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Resources;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        ICommand _signInTapCommand;
        ICommand _googleSignInTapCommand;
        ICommand _signUpTapCommand;
        ICommand _forgotPasswordTapCommand;
        IGoogleManager _googleManager;
        ILogInService _logInService;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;
        string _password;


        public LoginPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            IGoogleManager googleManager, ILogInService logInService)
            : base(navigationService, memoryCache)
        {
            _logInService = logInService;
            _googleManager = googleManager;
            _signInTapCommand = new Command(async () => await OnSignInTappedAsync());
            _googleSignInTapCommand = new Command(OnGoogleSignInTapped);
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

            var logInResponseDTO = await _logInService.LoginWithEmailAndPasswordAsync(Email, Password);

            if (logInResponseDTO.IsSuccess)
            {
                SetCurrentUser(logInResponseDTO.CurrentUser);
                await _navigationService.NavigateAsync("NavigationPage/HomePage");
            }
            else
            {
                ErrorMessageIsVisible = true;

                switch (logInResponseDTO.Status)
                {
                    case LogInResponseStatusEnum.EmailNotVerified:
                        ErrorMessageText = AppRes.EmailNotVerified;
                        break;

                    case LogInResponseStatusEnum.InvalidEmailOrPassword:
                        ErrorMessageText = AppRes.InvalidEmailOrPassword;
                        break;
                    case LogInResponseStatusEnum.Exception:
                        ErrorMessageText = logInResponseDTO.Message;
                        break;

                }
            }
        }

        private async Task OnSignUpTappedAsync()
        {

            await _navigationService.NavigateAsync("NavigationPage/SignUpPage");


        }

        private async Task OnForgotPasswordTappedAsync()
        {
            await _navigationService.NavigateAsync("NavigationPage/ForgotPasswordPage");
        }

        private void OnGoogleSignInTapped()
        {
            try
            {
                Action<GoogleUserDTO, string> loginCallBack = async (googleUserDTO, message) =>
                                        await OnGoogleLoginCompleteAsync(googleUserDTO, message);

                _logInService.LoginWithGoogleAuth(loginCallBack);
            }
            catch (Exception ex)
            {
                ErrorMessageIsVisible = true;
                ErrorMessageText = AppRes.GoogleAccountNotFound;
            }
        }

        private async Task OnGoogleLoginCompleteAsync(GoogleUserDTO googleUserDTO, string message)
        {
            if (googleUserDTO == null)
            {
                ErrorMessageIsVisible = true;
                ErrorMessageText = AppRes.GoogleAccountNotFound;
                return;
            }

            var logInResponseDTO = await _logInService.AddGoogleAccauntToFirebaseAsync(googleUserDTO);

            if (!logInResponseDTO.IsSuccess)
            {
                ErrorMessageIsVisible = true;
                ErrorMessageText = AppRes.GoogleAccountNotFound;
                return;
            }


            SetCurrentUser(logInResponseDTO.CurrentUser);
            await _navigationService.NavigateAsync("NavigationPage/HomePage");
        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
        }

    }
}
