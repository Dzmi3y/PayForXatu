using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService _navigationService { get; private set; }
        protected IMemoryCache _memoryCache;

        public CurrentUserDTO CurrentUser
        {
            get { return _memoryCache.Get("CurrentUser") as CurrentUserDTO; }
        }

        public ViewModelBase(INavigationService navigationService, IMemoryCache memoryCache)
        {
            _navigationService = navigationService;
            _memoryCache = memoryCache;

        }

        public void SetCurrentUser(CurrentUserDTO currentUser)
        {
            _memoryCache.Set("CurrentUser", currentUser);
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
