using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Controls.PaymentsList;
using PayForXatu.MAUIApp.Models;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private ObservableCollection<SearchPaymentItemModel> _searchPaymentsNameList;
        private ObservableCollection<HistoryPaymentItem> _historyPaymentList;
        public HistoryPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache,currencyService)
        {
            Title = "History";
            HistoryPaymentList = new ObservableCollection<HistoryPaymentItem>();
            SearchPaymentsNameList = new ObservableCollection<SearchPaymentItemModel>();

            Init();
        }

        private void Init()
        {

            SearchPaymentsNameList.Add(new SearchPaymentItemModel() { IsSelected = true, PaymentName = "First payment" });
            SearchPaymentsNameList.Add(new SearchPaymentItemModel() { IsSelected = false, PaymentName = "Second payment" });

            var cv = new ObservableCollection<CounterValueModel>();
            cv.Add(new CounterValueModel()
            {
                Title = "Counter #1",
                Value = 5
            });
            cv.Add(new CounterValueModel()
            {
                Title = "Counter #2",
                Value = 6
            });
            var cv2 = new ObservableCollection<CounterValueModel>();
            cv2.Add(new CounterValueModel()
            {
                Title = "Counter #1",
                Value = 7
            });


            var pl = new ObservableCollection<PaymentModel>();
            pl.Add(new PaymentModel()
            {
                Title = "First Payment",
                CounterValues = cv
            });
            pl.Add(new PaymentModel()
            {
                Title = "Second Payment",
                CounterValues = cv2
            });

            var t = new HistoryPaymentItem()
            {
                Title = DateTime.Now.ToString("dd.MM.yyyy"),
                Currency = CurrentUser.UserSettings.Currency.Name,
                IsExpanded = false,
                Amount = 23,
                PaymentsList = pl,
                SequenceNumber = 0
            };


            var cv21 = new ObservableCollection<CounterValueModel>();
            cv21.Add(new CounterValueModel()
            {
                Title = "Counter #1",
                Value = 9
            });
            cv21.Add(new CounterValueModel()
            {
                Title = "Counter #2",
                Value = 10
            });
            var cv22 = new ObservableCollection<CounterValueModel>();
            cv22.Add(new CounterValueModel()
            {
                Title = "Counter #1",
                Value = 11
            });
            cv22.Add(new CounterValueModel()
            {
                Title = "Counter #2",
                Value = 22
            });


            var pl2 = new ObservableCollection<PaymentModel>();
            pl2.Add(new PaymentModel()
            {
                Title = "First Payment",
                CounterValues = cv21
            });
            pl2.Add(new PaymentModel()
            {
                Title = "Second Payment",
                CounterValues = cv22
            });


            var t2 = new HistoryPaymentItem()
            {
                Title = DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy"),
                Currency = CurrentUser.UserSettings.Currency.Name,
                IsExpanded = false,
                Amount = 46,
                PaymentsList = pl2,
                SequenceNumber = 1
            };


            HistoryPaymentList.Add(t);
            HistoryPaymentList.Add(t2);
        }


        public ObservableCollection<SearchPaymentItemModel> SearchPaymentsNameList
        {
            get { return _searchPaymentsNameList; }
            set
            {
                SetProperty(ref _searchPaymentsNameList, value);
            }
        }

        public ObservableCollection<HistoryPaymentItem> HistoryPaymentList
        {
            get { return _historyPaymentList; }
            set
            {
                SetProperty(ref _historyPaymentList, value);
            }
        }

    }


    public class SearchPaymentItemModel : BindableBase
    {
        private bool _isSelected;

        public SearchPaymentItemModel()
        {
            ImageTapCommand = new Command(()=>IsSelected=!IsSelected);
        }

        public string PaymentName { get; set; }

        public ICommand ImageTapCommand { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

    }

    public class HistoryPaymentItem : BindableBase
    {
        private string _title;
        private string _currency;
        private double _amount;
        private bool _isExpanded;
        private bool _isEvenItem;
        private int _sequenceNumber;
        private ObservableCollection<PaymentModel> _paymentsList;

        public HistoryPaymentItem()
        {
            ExpandSwitchTappedCommand = new Command(() => { IsExpanded = !IsExpanded; });
            PaymentsList = new ObservableCollection<PaymentModel>();
        }

        public ICommand ExpandSwitchTappedCommand { get; set; }

        public bool IsEvenItem
        {
            get { return _isEvenItem; }
            private set { SetProperty(ref _isEvenItem, value); }
        }

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
            set
            {
                SetProperty(ref _sequenceNumber, value);
                IsEvenItem = value % 2 == 0;
            }
        }
        public ObservableCollection<PaymentModel> PaymentsList
        {
            get { return _paymentsList; }
            set
            {
                SetProperty(ref _paymentsList, value);
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                SetProperty(ref _amount, value);
            }
        }
        public string Currency
        {
            get { return _currency; }
            set
            {
                SetProperty(ref _currency, value);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

    }

}
