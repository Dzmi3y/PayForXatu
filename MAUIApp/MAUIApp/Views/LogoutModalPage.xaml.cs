using CommunityToolkit.Maui.Views;

namespace PayForXatu.MAUIApp.Views;

public partial class ConfirmModalPage : Popup
{
    private Action _confirm;
    public ConfirmModalPage(Action confirm, string textMessage)
    {
        InitializeComponent();
        _confirm = confirm;
        TextMessageLabel.Text = textMessage;
    }

    private void Close_Tapped(object sender, EventArgs e)
    {
        Close();
    }

    private void Confirm_Tapped(object sender, EventArgs e)
    {
        if (_confirm != null)
            _confirm.Invoke();
        Close();
    }
}
