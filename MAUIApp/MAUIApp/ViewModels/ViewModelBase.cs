using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
        private ICommand _tapMenuCommand;
        private string _title;

        public ViewModelBase(INavigationService navigationService, IMemoryCache memoryCache)
        {
            _navigationService = navigationService;
            _memoryCache = memoryCache;
            _tapMenuCommand = new Command(async (pageName) => { await TapMenuComandHandlerAsync((string)pageName); });

        }

        public async Task TapMenuComandHandlerAsync(string pageName)
        {
            await _navigationService.NavigateAsync(pageName);
        }

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


        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
