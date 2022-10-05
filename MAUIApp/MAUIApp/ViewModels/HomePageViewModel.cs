using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Controls.PaymentsList;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class HomePageViewModel :ViewModelBase
    {

        private ObservableCollection<Counter> _counters;
        public HomePageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache, currencyService)
        {
            Title = "Home";
            Counters = new ObservableCollection<Counter>();

     

           var counterValues = new ObservableCollection<CounterValue>();
            counterValues.Add(new CounterValue { Title = "Counter #1", Value = 123 });
            counterValues.Add(new CounterValue { Title = "Counter #2", Value = 456 });

            Counters.Add(new Counter
            {
                CounterValues = counterValues,
                IsFilledIn = false,
                IsExpanded = false,
                Title = "Counter 1",
                SequenceNumber = 0

            }) ;

            Counters.Add(new Counter
            {
                IsFilledIn = false,
                IsExpanded = false,
                Title = "Counter 2",
                SequenceNumber = 1

            });
        }

        public ObservableCollection<Counter> Counters
        {
            get { return _counters; }
            set { SetProperty(ref _counters, value); }
        }

    }

    public class CounterValue : BindableBase
    {
        private double _value;

        public event Action ValueWasChangedEvent;


        public string Title { get; set; }
        public double Value
        {
            get { return _value; }
            set {
                SetProperty(ref _value, value);
                if(ValueWasChangedEvent!=null)
                    ValueWasChangedEvent.Invoke();
            }
        }


    }

    public class Counter : BindableBase
    {
        private ObservableCollection<CounterValue> _counterValues;
        private string _title;
        private bool _isExpanded;
        private bool _isFilledIn;
        private double _paymentAmmountValue;
        private int _sequenceNumber;
        private bool _isEvenItem;

        public Counter()
        {
            ExpandSwitchTappedCommand = new Command( () => { IsExpanded = !IsExpanded; });
        }



        public ICommand ExpandSwitchTappedCommand { get; set; }

        public ObservableCollection<CounterValue> CounterValues
        {
            get { return _counterValues; }
            set {
                SetProperty(ref _counterValues, value);
                value.ToList().ForEach(x => x.ValueWasChangedEvent += OnChangedValue);
            }
        }


        public bool IsEvenItem
        {
            get { return _isEvenItem; }
            private set { SetProperty(ref _isEvenItem, value); }
        }

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
            set {
                SetProperty(ref _sequenceNumber, value);
                IsEvenItem = value%2==0;
            }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }
        public bool IsFilledIn
        {
            get { return _isFilledIn; }
            set { SetProperty(ref _isFilledIn, value); }
        }

        public double PaymentAmmountValue
        {
            get { return _paymentAmmountValue; }
            set {
                SetProperty(ref _paymentAmmountValue, value);
                IsFilledIn = value > 0;
            }
        }

        public void OnChangedValue()
        {
            PaymentAmmountValue = CounterValues.Select(x => x.Value).Sum();   
        }
    }
}
