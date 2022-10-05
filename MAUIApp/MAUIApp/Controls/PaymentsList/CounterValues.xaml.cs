using Microsoft.Maui.Controls.Platform;
using PayForXatu.MAUIApp.ViewModels;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.Controls.PaymentsList;

public partial class CounterValues : ContentView
{
	public CounterValues()
	{
		InitializeComponent();
    }

    public static readonly BindableProperty CounterValuesListProperty = BindableProperty.Create(
        "CounterValuesList", typeof(ObservableCollection<CounterValue>),
        typeof(ObservableCollection<CounterValue>), new ObservableCollection<CounterValue>());

    public ObservableCollection<CounterValue> CounterValuesList
    {
        get => (ObservableCollection<CounterValue>)GetValue(CounterValuesListProperty);
        set => SetValue(CounterValuesListProperty, value);
    }

}
