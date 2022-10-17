using Android.Icu.Util;
using PayForXatu.MAUIApp.Models;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.Controls.HistoryPaymentList;

public partial class HistoryPaymentControl : ContentView
{
	public HistoryPaymentControl()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty PaymentsListProperty = BindableProperty.Create(
    "PaymentsList", typeof(ObservableCollection<PaymentModel>),
    typeof(ObservableCollection<PaymentModel>), new ObservableCollection<PaymentModel>());

    public ObservableCollection<PaymentModel> PaymentsList
    {
        get => (ObservableCollection<PaymentModel>)GetValue(PaymentsListProperty);
        set => SetValue(PaymentsListProperty, value);
    }


    public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
   "IsExpanded", typeof(bool), typeof(bool), false);

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly BindableProperty CurrencyProperty = BindableProperty.Create(
   "Currency", typeof(string), typeof(string), string.Empty);

    public string Currency
    {
        get => (string)GetValue(CurrencyProperty);
        set => SetValue(CurrencyProperty, value);
    }
    
}
