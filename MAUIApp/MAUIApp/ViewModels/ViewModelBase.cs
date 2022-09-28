using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.BusinessLogic.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService _navigationService { get; private set; }
        protected IMemoryCache _memoryCache;
        protected ICurrencyService _currencyService;
        bool _menuIsOpen;
        bool _flashlightIsOn;
        private ICommand _tapMenuCommand;
        private string _title;

        public ViewModelBase(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
        {
            _navigationService = navigationService;
            _memoryCache = memoryCache;
            _tapMenuCommand = new Command(async (pageName) => { await TapMenuComandHandlerAsync((string)pageName); });
            _currencyService = currencyService;
        }

        public async Task TapMenuComandHandlerAsync(string pageName)
        {
            if (pageName != Title)
            {
                var param = new NavigationParameters()
                {
                    {"MenuIsOpen", true},
                    {"FlashlightIsOn", FlashlightIsOn}
                };

                if (pageName == "LoginPage")
                {
                    if (OpenLogoutModal != null)
                    {
                        OpenLogoutModal.Invoke(async ()=>await LogoutAsync());
                    }

                    return;
                }

                await _navigationService.NavigateAsync("NavigationPage/" + pageName, param);
            }
        }

        private async Task LogoutAsync()
        {
            FlashlightIsOn = false;
            SetCurrentUser(null);
                
            await _navigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        public Action<Action> OpenLogoutModal { get; set; }

        public CurrentUserDTO CurrentUser
        {
            get { return _memoryCache.Get("CurrentUser") as CurrentUserDTO; }
        }

        public ICommand TapMenuCommand
        {
            get { return _tapMenuCommand; }
        }

        public void SetCurrentUser(CurrentUserDTO currentUser)
        {
            _memoryCache.Set("CurrentUser", currentUser);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool FlashlightIsOn
        {
            get { return _flashlightIsOn; }
            set
            {
                SetProperty(ref _flashlightIsOn, value);
                Task task = ToggleFlashlight(value);
            }
        }
        public bool MenuIsOpen
        {
            get { return _menuIsOpen; }
            set { SetProperty(ref _menuIsOpen, value); }
        }

        private async Task ToggleFlashlight(bool isToggle)
        {
            try
            {
                if (isToggle)
                    await Flashlight.Default.TurnOnAsync();
                else
                    await Flashlight.Default.TurnOffAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
            {
                MenuIsOpen = false;
                FlashlightIsOn = false;
            }
            if (parameters.Any(x => x.Key == "MenuIsOpen"))
            {
                var menuIsOpen = parameters.FirstOrDefault(x => x.Key == "MenuIsOpen");
                MenuIsOpen = (bool)menuIsOpen.Value;
            }
            else
            {
                MenuIsOpen = false;
            }

            if (parameters.Any(x => x.Key == "FlashlightIsOn"))
            {
                var flashlightIsOn = parameters.FirstOrDefault(x => x.Key == "FlashlightIsOn");
                FlashlightIsOn = (bool)flashlightIsOn.Value;
            }
        }

        public virtual void Destroy()
        {

        }
    }
}
