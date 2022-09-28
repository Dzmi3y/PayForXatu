using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public HistoryPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache,currencyService)
        {
            Title = "History";
        }

       
    }
}
