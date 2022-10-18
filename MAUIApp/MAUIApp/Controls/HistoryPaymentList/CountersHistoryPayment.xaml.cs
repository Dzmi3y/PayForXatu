using PayForXatu.Database.Models;
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
       "CounterValues", typeof(List<Counter>),
       typeof(List<Counter>), new List<Counter>());

    public List<Counter> CounterValues
    {
        get => (List<Counter>)GetValue(CounterValuesProperty);
        set => SetValue(CounterValuesProperty, value);
    }
}
