using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database.Models;
using PayForXatu.MAUIApp.Models;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private ObservableCollection<SearchPaymentItemModel> _searchPaymentsNameList;
        private ObservableCollection<SearchPaymentItemModel> _viewSearchPaymentsNameList;
        private ObservableCollection<HistoryPaymentItemModel> _historyPaymentList;
        private IHistoryPaymentService _historyPaymentService;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _dateIsSetup;
        private string _searchKey;
        public HistoryPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService, IHistoryPaymentService historyPaymentService)
            : base(navigationService, memoryCache,currencyService)
        {
            Title = "History";
            
            HistoryPaymentList = new ObservableCollection<HistoryPaymentItemModel>();
            SearchPaymentsNameList = new ObservableCollection<SearchPaymentItemModel>();
            _historyPaymentService = historyPaymentService;

            _dateIsSetup = false;
            StartDate = DateTime.Now.AddMonths(-2);
            EndDate = DateTime.Now;
            _dateIsSetup = true;
            _ = LoadPaymentNamesListAsync();

        }

        private async Task LoadPaymentNamesListAsync()
        {
            if (!_dateIsSetup)
                return;
            var paymentNamesList = await _historyPaymentService.GetPaymentNamesAsync(CurrentUser.UserId);
            SearchPaymentsNameList.Clear();
            paymentNamesList.ForEach(x=>
                SearchPaymentsNameList.Add(
                    new SearchPaymentItemModel()
                        {
                            IsSelected=false,
                            PaymentName=x,
                            Switched= async () => { await LoadPaymentsHistory(); }
                    }));
            ViewSearchPaymentsNameList = SearchPaymentsNameList;
            await LoadPaymentsHistory();
        }

        private async Task LoadPaymentsHistory()
        {
            var selectedPaymentsNames = SearchPaymentsNameList.Where(x=>x.IsSelected)
                  .Select(x=>x.PaymentName)
                  .ToList();

             var paymentHistoryDictionarey = await _historyPaymentService
                .GetPaymentsByNamesListAndPeriodAsync(CurrentUser.UserId,
                        StartDate, EndDate, selectedPaymentsNames);


            HistoryPaymentList= new ObservableCollection<HistoryPaymentItemModel>();

            int i = 0;
            foreach (var currentItem in paymentHistoryDictionarey)
            { 
                var newItem = new HistoryPaymentItemModel();
                newItem.Title = currentItem.Key.ToString("dd.MM.yyyy");
                newItem.Currency = CurrentUser.UserSettings.Currency.Name;
                newItem.PaymentsList = new ObservableCollection<Payment>();
                currentItem.Value.ForEach(p=>newItem.PaymentsList.Add(p));
                newItem.Amount = 0;
                currentItem.Value.ForEach(p => newItem.Amount += p.Amount);
                newItem.SequenceNumber = i++;

                HistoryPaymentList.Add(newItem);
            }
        }

        private void Search()
        {
            ViewSearchPaymentsNameList = SearchPaymentsNameList
                .Where(x => x.PaymentName.Contains(SearchKey))
                .ToObservableCollection();
        }
        
        public ObservableCollection<SearchPaymentItemModel> ViewSearchPaymentsNameList
        {
            get { return _viewSearchPaymentsNameList; }
            set
            {
                SetProperty(ref _viewSearchPaymentsNameList, value);
            }
        }


        public ObservableCollection<SearchPaymentItemModel> SearchPaymentsNameList
        {
            get { return _searchPaymentsNameList; }
            set
            {
                SetProperty(ref _searchPaymentsNameList, value);
            }
        }

        public ObservableCollection<HistoryPaymentItemModel> HistoryPaymentList
        {
            get { return _historyPaymentList; }
            set
            {
                SetProperty(ref _historyPaymentList, value);
            }
        }

        public string SearchKey
        {
            get { return _searchKey; }
            set
            {
                SetProperty(ref _searchKey, value);
                Search();
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set {
                SetProperty(ref _startDate, value);
                _ = LoadPaymentsHistory();
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set {
                SetProperty(ref _endDate, value);
                _ = LoadPaymentsHistory();
            }
        }

    }
}
