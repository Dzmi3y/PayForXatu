using System.Windows.Input;

namespace PayForXatu.MAUIApp.Controls;

public partial class BlackButtonPFX : ContentView
{
	public BlackButtonPFX()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
         "Text", typeof(string), typeof(string), "");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
     "TapCommand", typeof(ICommand), typeof(ICommand));

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
}
