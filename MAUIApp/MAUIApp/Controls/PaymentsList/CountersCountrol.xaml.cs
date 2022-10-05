using PayForXatu.MAUIApp.ViewModels;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.Controls.PaymentsList;

public partial class CountersCountrol : ContentView
{
	public CountersCountrol()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty CountersProperty = BindableProperty.Create(
        "Counters", typeof(ObservableCollection<Counter>),
        typeof(ObservableCollection<Counter>), new ObservableCollection<Counter>());

    public ObservableCollection<Counter> Counters
    {
        get => (ObservableCollection<Counter>)GetValue(CountersProperty);
        set => SetValue(CountersProperty, value);
    }
}
