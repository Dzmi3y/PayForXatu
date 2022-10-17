using PayForXatu.MAUIApp.Models;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.Controls.HistoryPaymentList;

public partial class CountersHistoryPayment : ContentView
{
	public CountersHistoryPayment()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty CounterValuesProperty = BindableProperty.Create(
       "CounterValues", typeof(ObservableCollection<CounterValueModel>),
       typeof(ObservableCollection<CounterValueModel>), new ObservableCollection<CounterValueModel>());

    public ObservableCollection<CounterValueModel> CounterValues
    {
        get => (ObservableCollection<CounterValueModel>)GetValue(CounterValuesProperty);
        set => SetValue(CounterValuesProperty, value);
    }
}
