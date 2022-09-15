﻿using Firebase.Auth;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.Database;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Web;
using Firebase;
using Firebase.Database;
using PayForXatu.BusinessLogic;
using System.Xml.Linq;
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
        IMemoryCache _memoryCache;
        IFirebaseRepository _firebaseRepository;
        IGoogleManager _googleManager;
        ILogInService _logInService;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;
        string _password;


        public LoginPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            IFirebaseRepository firebaseRepository, IGoogleManager googleManager, ILogInService logInService)
            : base(navigationService)
        {
            _logInService = logInService;
            _googleManager = googleManager;
            _firebaseRepository = firebaseRepository;
            _memoryCache = memoryCache;
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
                await _navigationService.NavigateAsync("MainPage");
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

            await _navigationService.NavigateAsync("SignUpPage");


        }

        private async Task OnForgotPasswordTappedAsync()
        {
            await _navigationService.NavigateAsync("ForgotPasswordPage");
        }

        private void OnGoogleSignInTapped()
        {
            Action<GoogleUserDTO, string> loginCallBack = async (googleUserDTO, message) =>
                                    await OnGoogleLoginCompleteAsync(googleUserDTO, message);

            var logInResponseDTO = _logInService.LoginWithGoogleAuth(loginCallBack);

            if (!logInResponseDTO.IsSuccess)
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

            await _navigationService.NavigateAsync("MainPage");
        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
        }

    }
}