using Microsoft.Maui.Controls;
using PayForXatu.MAUIApp.Models;
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
        "Counters", typeof(ObservableCollection<PaymentModel>),
        typeof(ObservableCollection<PaymentModel>), new ObservableCollection<PaymentModel>());

    public ObservableCollection<PaymentModel> Counters
    {
        get => (ObservableCollection<PaymentModel>)GetValue(CountersProperty);
        set => SetValue(CountersProperty, value);
    }

    public static readonly BindableProperty OpenEditGridCommandProperty = BindableProperty.Create(
        "OpenEditGridCommand", typeof(Command),typeof(Command), null);

    public Command OpenEditGridCommand
    {
        get => (Command)GetValue(OpenEditGridCommandProperty);
        set => SetValue(OpenEditGridCommandProperty, value);
    }

}
