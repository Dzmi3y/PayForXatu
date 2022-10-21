using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database.Models;
using PayForXatu.MAUIApp.Models.Chart;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class AnalyticsPageViewModel : ViewModelBase
    {
        IHistoryPaymentService _historyPaymentService;
        private ObservableCollection<string> _paymentsNamesList;
        private ObservableCollection<string> _countersNamesList;
        private string _countersNamesSelectedItem;
        private string _paymentsNamesSelectedItem;
        private DateTime _endDate;
        private DateTime _startDate;
        private GraphicsDrawable _paymentsChart;
        private GraphicsDrawable _countersChart;
        private double _paymentChartWidth;
        private double _counterChartWidth;
        private bool _countersIsVisible;
        private bool _paymentsIsVisible;
        private List<Payment> _paymentsHistory;

        public AnalyticsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService, IHistoryPaymentService historyPaymentService)
            : base(navigationService, memoryCache, currencyService)
        {
            Title = "Analytics";
            _historyPaymentService = historyPaymentService;
            StartDate = DateTime.Now.AddMonths(-2);
            EndDate = DateTime.Now;
            PaymentsNamesList = new ObservableCollection<string>();
            CountersNamesList = new ObservableCollection<string>();
            CounterChartWidth = 500;
            PaymentChartWidth = 500;
            CountersIsVisible = false;
            PaymentsIsVisible = false;

            _ = LoadPaymentNamesAsync();
        }

        private void UpdatePaymentsChart(List<BarInfo> paymentBars)
        {
            PaymentsChart = new GraphicsDrawable(paymentBars, Color.Parse("#345995"),
            CurrentUser.UserSettings.Currency.Name)
            {
                ChangeWidthAction = (width) =>
                {
                    PaymentChartWidth = width;
                }
            };
        }

        private void UpdateCountersChart(List<BarInfo> counterBars)
        {
            CountersChart = new GraphicsDrawable(counterBars, Color.Parse("#ffc300"))
            {
                ChangeWidthAction = (width) =>
                {
                    CounterChartWidth = width;
                }
            };
        }

        private async Task LoadPaymentNamesAsync()
        {
            PaymentsNamesList.Clear();
            var paymentsNames = await _historyPaymentService.GetPaymentNamesAsync(CurrentUser.UserId);
            if (paymentsNames.Count > 0)
            {
                PaymentsNamesList = paymentsNames.ToObservableCollection();
                PaymentsNamesSelectedItem = paymentsNames.FirstOrDefault();
                PaymentsIsVisible = true;
            }
            else
            {
                PaymentsIsVisible = false;
            }
        }


        private async Task LoadSelectedPaymentAsync(string paymentName)
        {
            if (string.IsNullOrEmpty(paymentName))
                return;
            _paymentsHistory = await _historyPaymentService
                .GetPaymentHistoryByNameAndPeriodAsync(CurrentUser.UserId,
                    StartDate, EndDate, paymentName);

            if (_paymentsHistory.Count == 0)
            {
                CountersIsVisible = false;
                PaymentsIsVisible = false;
            }
            else
            {
                PaymentsIsVisible = true;
            }

            CountersNamesList.Clear();

            var paymentsChartData = new List<BarInfo>();
            var countersList = new List<Counter>();

            _paymentsHistory.ForEach(p =>
            {
                countersList.AddRange(p.Counters);
                paymentsChartData.Add(new BarInfo((float)p.Amount, p.Date));
            });

            CountersNamesList = countersList.Select(c => c.CounterName)
                .Distinct()
                .ToObservableCollection();
            if (CountersNamesList.Count > 0)
            {
                CountersNamesSelectedItem = CountersNamesList.FirstOrDefault();
                CountersIsVisible = true;
            }
            else
            {
                CountersIsVisible = false;
            }

            if (paymentsChartData.Count > 0) 
                UpdatePaymentsChart(paymentsChartData);
        }

        private void LoadSelectedCounter(string counterName)
        {
            if (string.IsNullOrEmpty(counterName))
                return;

            if (_paymentsHistory.Count == 0)
            {
                CountersIsVisible = false;
                return;
            }

            var countersChartData = new List<BarInfo>();

            _paymentsHistory.ForEach(p =>
            {
                var counters = p.Counters
                .Where(c => c.CounterName == counterName)
                .Select(c => new BarInfo((float)c.CounterValue, p.Date))
                .ToList();
                countersChartData.AddRange(counters);
            });

            CountersIsVisible = true;
            UpdateCountersChart(countersChartData);
        }

        public string PaymentsNamesSelectedItem
        {
            get { return _paymentsNamesSelectedItem; }
            set
            {
                SetProperty(ref _paymentsNamesSelectedItem, value);
                _ = LoadSelectedPaymentAsync(value);
            }
        }

        public ObservableCollection<string> PaymentsNamesList
        {
            get { return _paymentsNamesList; }
            set { SetProperty(ref _paymentsNamesList, value); }
        }

        public string CountersNamesSelectedItem
        {
            get { return _countersNamesSelectedItem; }
            set
            {
                SetProperty(ref _countersNamesSelectedItem, value);
                LoadSelectedCounter(value);
            }
        }

        public ObservableCollection<string> CountersNamesList
        {
            get { return _countersNamesList; }
            set { SetProperty(ref _countersNamesList, value); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
                _ = LoadSelectedPaymentAsync(PaymentsNamesSelectedItem);
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetProperty(ref _endDate, value);
                _ = LoadSelectedPaymentAsync(PaymentsNamesSelectedItem);
            }
        }

        public bool CountersIsVisible
        {
            get { return _countersIsVisible; }
            set { SetProperty(ref _countersIsVisible, value); }
        }

        public bool PaymentsIsVisible
        {
            get { return _paymentsIsVisible; }
            set { SetProperty(ref _paymentsIsVisible, value); }
        }

        public GraphicsDrawable PaymentsChart
        {
            get { return _paymentsChart; }
            set { SetProperty(ref _paymentsChart, value); }
        }

        public GraphicsDrawable CountersChart
        {
            get { return _countersChart; }
            set { SetProperty(ref _countersChart, value); }
        }

        public double PaymentChartWidth
        {
            get { return _paymentChartWidth; }
            set { SetProperty(ref _paymentChartWidth, value); }
        }

        public double CounterChartWidth
        {
            get { return _counterChartWidth; }
            set { SetProperty(ref _counterChartWidth, value); }
        }
    }
}
