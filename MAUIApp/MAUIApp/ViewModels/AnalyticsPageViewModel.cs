using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class AnalyticsPageViewModel : ViewModelBase
    {

        public AnalyticsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache,currencyService)
        {
            Title = "Analytics";
        }


    }
}
