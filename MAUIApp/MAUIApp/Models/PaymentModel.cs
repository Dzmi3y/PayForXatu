using Android.Icu.Util;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.Models
{
    public class PaymentModel : BindableBase
    {
        private ObservableCollection<CounterValueModel> _counterValues;
        private string _title;
        private bool _isExpanded;
        private bool _isFilledIn;
        private double _paymentAmountValue;
        private int _sequenceNumber;
        private bool _isEvenItem;

        public PaymentModel()
        {
            ExpandSwitchTappedCommand = new Command(() => { IsExpanded = !IsExpanded; });
            CounterValues = new ObservableCollection<CounterValueModel>();
        }

        public Guid TemplatePaymentId { get; set; }
        public Guid PaymentId { get; set; }

        public ICommand ExpandSwitchTappedCommand { get; set; }

        public ObservableCollection<CounterValueModel> CounterValues
        {
            get { return _counterValues; }
            set
            {
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
            set
            {
                SetProperty(ref _sequenceNumber, value);
                IsEvenItem = value % 2 == 0;
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

        public double PaymentAmountValue
        {
            get { return _paymentAmountValue; }
            set
            {
                SetProperty(ref _paymentAmountValue, value);
                IsFilledIn = value > 0;
            }
        }

        public void CounterValuesWasUpdated()
        {
            CounterValues.ToList().ForEach(x => x.ValueWasChangedEvent += OnChangedValue);
        }
        public void OnChangedValue()
        {
            PaymentAmountValue = CounterValues.Select(x => x.Value).Sum();
        }
        public void ClearCounters()
        {
            foreach (var counter in CounterValues)
            {
                counter.Value = 0;
            }
            PaymentAmountValue = 0;
            PaymentId = Guid.NewGuid();
        }
    }
}
