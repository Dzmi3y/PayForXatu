namespace PayForXatu.MAUIApp.Controls;

public partial class SearchEntryPFX : ContentView
{
    public SearchEntryPFX()
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
        "Text", typeof(string), typeof(string), "");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        "IsPassword", typeof(bool), typeof(bool), false);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }
}
