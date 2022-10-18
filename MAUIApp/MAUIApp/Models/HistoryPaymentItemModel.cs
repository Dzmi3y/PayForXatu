using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayForXatu.MAUIApp.Models
{
    public class HistoryPaymentItemModel : BindableBase
    {
        private string _title;
        private string _currency;
        private double _amount;
        private bool _isExpanded;
        private bool _isEvenItem;
        private int _sequenceNumber;
        private ObservableCollection<Payment> _paymentsList;

        public HistoryPaymentItemModel()
        {
            ExpandSwitchTappedCommand = new Command(() => { IsExpanded = !IsExpanded; });
            PaymentsList = new ObservableCollection<Payment>();
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
        public ObservableCollection<Payment> PaymentsList
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
