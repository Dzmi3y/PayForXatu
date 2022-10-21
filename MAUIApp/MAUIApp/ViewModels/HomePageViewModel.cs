using Microsoft.Extensions.Caching.Memory;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.Database.Models;
using PayForXatu.MAUIApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class HomePageViewModel :ViewModelBase
    {

        private ObservableCollection<PaymentModel> _counters;
        private ObservableCollection<CounterValueModel> _selectedCounterValueList;
        private string _paymentName;
        private bool _editModeIsVisible;
        private bool _isEditMode;
        private PaymentModel _selectedCounter;
        private IHistoryPaymentService _historyPaymentService;
        private IPaymentService _paymentService;
        public HomePageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService, IHistoryPaymentService historyPaymentService,
            IPaymentService paymentService)
            : base(navigationService, memoryCache, currencyService)
        {
            Title = "Home";
            _historyPaymentService = historyPaymentService;
            _paymentService= paymentService;

            SavePaymentButtonCommand = new Command(() => {
                if (OpenSavePaymentDataModal != null)
                    OpenSavePaymentDataModal.Invoke(SavePaymentData);
            });

            CloseEditGridCommand = new Command(() => {
                if (CloseEditGridModal != null)
                    CloseEditGridModal.Invoke(CloseEditGrid);
            });

            RemovePaymentDataCommand = new Command(() => {
                if (RemovePaymentDataModal != null)
                    RemovePaymentDataModal.Invoke(async () => await RemovePaymentDataAsync());
            });

            SaveChangesPaymentDataCommand = new Command(() => {
                if (SaveChangesPaymentDataModal != null)
                    SaveChangesPaymentDataModal.Invoke(async () => await SaveChangesPaymentDataAsync());
            });

            RemovePaymentItemCommand = new Command((param) => { RemovePaymentItem(param); });
            AddCounterItemCommand = new Command(AddCounterItem);
            OpenEditGridCommand = new Command((param) => OpenEditGrid(param));
            
            Counters = new ObservableCollection<PaymentModel>();

            PaymentName = string.Empty;
            Task fillPaymentListTask = FillPaymentListAsync();

            Counters.CollectionChanged += Counters_CollectionChanged;
        }


        private void Counters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (var i = 0; i < Counters.Count; i++)
            {
                Counters[i].SequenceNumber = i;
            }
        }

        private async Task FillPaymentListAsync()
        {
            var payments = await _paymentService.GetSavedPaymentsAsync(CurrentUser.UserId);
            foreach (var payment in payments)
            {
                var counterValues = new ObservableCollection<CounterValueModel>();
                var counterValuesList = payment.Counters
                    .Select((counterName) => new CounterValueModel() { Title = counterName })
                    .ToList<CounterValueModel>();
                counterValuesList.ForEach((cvm)=>counterValues.Add(cvm));

                var counter = new PaymentModel()
                {
                    CounterValues = counterValues,
                    IsFilledIn = false,
                    IsExpanded = false,
                    TemplatePaymentId = payment.Id,
                    PaymentId = Guid.NewGuid(),
                    Title = payment.PaymentName,
                };
                Counters.Add(counter);
            }
            
        }

        public ObservableCollection<CounterValueModel> SelectedCounterValueList
        {
            get { return _selectedCounterValueList; }
            set { SetProperty(ref _selectedCounterValueList, value); }
        }

        public ObservableCollection<PaymentModel> Counters
        {
            get { return _counters; }
            set { SetProperty(ref _counters, value); }
        }

        public string PaymentName
        {
            get { return _paymentName; }
            set { SetProperty(ref _paymentName, value); }
        }

        public bool EditModeIsVisible
        {
            get { return _editModeIsVisible; }
            set { SetProperty(ref _editModeIsVisible, value); }
        }

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { SetProperty(ref _isEditMode, value); }
        }

        public Action<Action> OpenSavePaymentDataModal { get; set; }
        public Action<Action> CloseEditGridModal { get; set; }
        public Action<Action> SaveChangesPaymentDataModal { get; set; }
        public Action<Action> RemovePaymentDataModal { get; set; }

        public ICommand RemovePaymentItemCommand { get; set; }
        public ICommand SavePaymentButtonCommand { get; set; }
        public ICommand AddCounterItemCommand { get; set; }
        public ICommand OpenEditGridCommand { get; set; }
        public ICommand CloseEditGridCommand { get; set; }
        public ICommand RemovePaymentDataCommand { get; set; }
        public ICommand SaveChangesPaymentDataCommand { get; set; }

        private void OpenEditGrid(Object param)
        {
            _selectedCounter = param as PaymentModel;
            IsEditMode = _selectedCounter != null;
            EditModeIsVisible = true;
            if (IsEditMode)
            {
                SelectedCounterValueList = new ObservableCollection<CounterValueModel>();
                PaymentName = _selectedCounter.Title;
                _selectedCounter.CounterValues.ToList().ForEach((cv) => SelectedCounterValueList.Add(cv));
            }
            else
            {
                PaymentName = String.Empty;

                _selectedCounter = new PaymentModel()
                {
                    TemplatePaymentId = Guid.NewGuid(),
                    CounterValues = new ObservableCollection<CounterValueModel>()
                };

                SelectedCounterValueList = _selectedCounter.CounterValues;
            }
        }

        private void CloseEditGrid()
        {
            EditModeIsVisible = false;
            _selectedCounter = null;
        }

        private void AddCounterItem()
        {
            SelectedCounterValueList.Add(new CounterValueModel() { CounterId = Guid.NewGuid()});
        }

        private  void RemovePaymentItem(object param)
        {
            var counterName = (CounterValueModel)param;
            SelectedCounterValueList.Remove(counterName);
        }

        private void SavePaymentData()
        {
            var paymentsList = Counters.Where(c => c.IsFilledIn)
                .Select(c => new Payment()
                {
                    Id = c.TemplatePaymentId,
                    UserId = CurrentUser.UserId,
                    Amount = c.PaymentAmountValue,
                    Date = DateTime.Now,
                    PaymentName = c.Title,
                    Counters = c.CounterValues
                        .Select(cv => new Counter() {
                            CounterName = cv.Title,
                            CounterValue = cv.Value })
                        .ToList()
                }).ToList();

            _historyPaymentService.AddPaymentsListAsync(paymentsList);

            foreach (var payment in Counters.Where(c=>c.IsFilledIn))
            {
                payment.ClearCounters();
            }

        }

        private async Task SaveChangesPaymentDataAsync()
        {
            for (var i = 0; i < _selectedCounter.CounterValues.Count; i++)
            {
                if (string.IsNullOrEmpty(_selectedCounter.CounterValues[i].Title))
                    _selectedCounter.CounterValues[i].Title =$"Counter #{i}";
            }

            if (IsEditMode)
            {
                _selectedCounter.Title = string.IsNullOrEmpty(PaymentName) ?
                    DateTime.Now.ToString():
                    PaymentName;

                _selectedCounter.CounterValues = SelectedCounterValueList;

                var savedPayment = await _paymentService.GetSavedPaymentByIdAsync(_selectedCounter.TemplatePaymentId);
                savedPayment.PaymentName = PaymentName;
                savedPayment.Counters = _selectedCounter.CounterValues
                                             .Select((cv) => cv.Title)
                                             .ToList();

                await _paymentService.UpdatePaymentAsync(savedPayment);
            }
            else
            {
                _selectedCounter.Title = string.IsNullOrEmpty(PaymentName) ?
                    DateTime.Now.ToString():
                    PaymentName;

                var newPaymentTemplate = new SavedPayment()
                {
                    Id=_selectedCounter.TemplatePaymentId,
                    UserId=CurrentUser.UserId,
                    PaymentName= _selectedCounter.Title,
                    Counters=_selectedCounter.CounterValues
                                             .Select((cv) => cv.Title)
                                             .ToList()
                };

                await _paymentService.AddPaymentAsync(newPaymentTemplate);
                Counters.Add(_selectedCounter);
            }
            CloseEditGrid();
        }
        private async Task RemovePaymentDataAsync()
        {
            Counters.Remove(_selectedCounter);
            await _paymentService.RemovePaymentAsync(_selectedCounter.TemplatePaymentId);
            CloseEditGrid();
        }
    }
}
