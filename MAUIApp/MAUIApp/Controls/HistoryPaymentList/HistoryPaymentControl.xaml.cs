using Android.Icu.Util;
using PayForXatu.Database.Models;
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
    "PaymentsList", typeof(ObservableCollection<Payment>),
    typeof(ObservableCollection<Payment>), new ObservableCollection<Payment>());

    public ObservableCollection<Payment> PaymentsList
    {
        get => (ObservableCollection<Payment>)GetValue(PaymentsListProperty);
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
