﻿using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Resources;
using System.Windows.Input;


namespace PayForXatu.MAUIApp.ViewModels
{
    public class SignUpPageViewModel:ViewModelBase
    {
        ICommand _backButtonTapCommand;
        ICommand _okButtonTapCommand;
        ISignUpService _authService;
        bool _errorMessageIsVisible;
        string _errorMessageText;
        string _email;
        string _password;
        string _confirmPassword;

        public SignUpPageViewModel(INavigationService navigationService, 
            ISignUpService authService,IMemoryCache memoryCache, ICurrencyService currencyService)
            : base(navigationService,memoryCache, currencyService)
        {
            _authService = authService;
            _backButtonTapCommand = new Command(async () => await OnBackButtonTappedAsync());
            _okButtonTapCommand = new Command(async () => await OnOkButtonTappedAsync());

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
            await _navigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        private async Task OnOkButtonTappedAsync()
        {
            var signUpUser = new SignUpUserDTO()
            {
                Email = _email,
                Password = _password,
                ConfirmPassword = _confirmPassword,
            };

            var serviceResponse = await _authService.SignUpAsync(signUpUser);

            ErrorMessageIsVisible = !serviceResponse.IsSuccess;

            if (serviceResponse.IsSuccess)
            {
                OpenModal.Invoke();
            }
            else
            {
                switch (serviceResponse.Status)
                {
                    case SignUpResponseStatusEnum.FieldsAreNotFilledIn:
                        ErrorMessageText = AppRes.FieldsAreNotFilledIn;
                        break;

                    case SignUpResponseStatusEnum.IncorrectEmail:
                        ErrorMessageText = AppRes.IncorrectEmail;
                        break;

                    case SignUpResponseStatusEnum.PasswordsDoNotMatch:
                        ErrorMessageText = AppRes.PasswordsDoNotMatch;
                        break;

                    case SignUpResponseStatusEnum.UserAlreadyExists:
                        ErrorMessageText = AppRes.UserAlreadyExists;
                        break;

                    case SignUpResponseStatusEnum.Exception:
                        ErrorMessageText = serviceResponse.Message;
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
