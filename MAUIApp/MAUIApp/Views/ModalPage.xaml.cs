using CommunityToolkit.Maui.Views;

namespace PayForXatu.MAUIApp.Views;

public partial class ModalPage : Popup
{
    private Action _close;
    public ModalPage(Action close, string textMessage)
    {
        InitializeComponent();
        _close = close;
        TextMessageLabel.Text = textMessage;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Close();
        if (_close != null)
            _close.Invoke();
    }
}
