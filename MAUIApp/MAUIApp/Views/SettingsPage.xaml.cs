using PayForXatu.MAUIApp.Controls;

namespace PayForXatu.MAUIApp.Views;

public partial class SettingsPage : BasePage
{
	public SettingsPage()
	{
        InitializeComponent();
    }

    private void PickupGrid_Tapped(object sender, EventArgs e)
    {
        if (CurrencyPicker.IsFocused)
            CurrencyPicker.Unfocus();
        CurrencyPicker.Focus();
    }
}
