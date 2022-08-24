using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayForXatu.MobilApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalPage : ContentPage
    {
        private Action _close;
        public ModalPage(Action close, string textMessage)
        {
            InitializeComponent();
            _close = close;
            MainGrid.FadeTo(0.6, 1000);
            TextMessageLabel.Text= textMessage;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MainGrid.FadeTo(0, 100);
            if (_close != null)
            _close.Invoke();
        }
    }
}