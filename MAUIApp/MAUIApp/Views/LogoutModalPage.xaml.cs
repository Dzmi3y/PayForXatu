using CommunityToolkit.Maui.Views;

namespace PayForXatu.MAUIApp.Views;

public partial class LogoutModalPage : Popup
{
    private Action _logout;
    public LogoutModalPage(Action logout, string textMessage)
    {
        InitializeComponent();
        _logout = logout;
        TextMessageLabel.Text = textMessage;
    }

    private void Close_Tapped(object sender, EventArgs e)
    {
        Close();
    }

    private void Logout_Tapped(object sender, EventArgs e)
    {
        if (_logout != null)
            _logout.Invoke();
        Close();
    }
}
