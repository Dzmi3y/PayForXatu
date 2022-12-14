using Microsoft.Maui;

namespace PayForXatu.MAUIApp.Controls;

public partial class WhiteEntryPFX : ContentView
{
	public WhiteEntryPFX()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
         "Placeholder", typeof(string), typeof(string), "");

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        "Text", typeof(string), typeof(string), "0");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set {

            SetValue(TextProperty,  string.IsNullOrEmpty(value)? "0":value) ;
        }
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        "IsPassword", typeof(bool), typeof(bool), false);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
       "Keyboard", typeof(Keyboard), typeof(Keyboard),Keyboard.Text);

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }
    
}
