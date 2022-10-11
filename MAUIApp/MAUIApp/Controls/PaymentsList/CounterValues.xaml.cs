using Microsoft.Maui.Controls.Platform;
using PayForXatu.MAUIApp.Models;
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
        "CounterValuesList", typeof(ObservableCollection<CounterValueModel>),
        typeof(ObservableCollection<CounterValueModel>), new ObservableCollection<CounterValueModel>());

    public ObservableCollection<CounterValueModel> CounterValuesList
    {
        get => (ObservableCollection<CounterValueModel>)GetValue(CounterValuesListProperty);
        set => SetValue(CounterValuesListProperty, value);
    }

}
