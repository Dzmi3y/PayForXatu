using Android.Text;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Models;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class AnalyticsPageViewModel : ViewModelBase
    {
        private ObservableCollection<string> _paymentsNamesList;
        private DateTime _endDate;
        private DateTime _startDate;
        private string _selectedItem;
        public AnalyticsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache,currencyService)
        {
            Title = "Analytics";
            StartDate = DateTime.Now.AddDays(-2);
            EndDate = DateTime.Now;
            PaymentsNamesList = new ObservableCollection<string>();

            for (var i = 0; i < 50; i++)
            {
                PaymentsNamesList.Add($"p{i}");
            }
           

            SelectedItem = PaymentsNamesList.FirstOrDefault();
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        public ObservableCollection<string> PaymentsNamesList
        {
            get { return _paymentsNamesList; }
            set
            {
                SetProperty(ref _paymentsNamesList, value);
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }

    }
}
