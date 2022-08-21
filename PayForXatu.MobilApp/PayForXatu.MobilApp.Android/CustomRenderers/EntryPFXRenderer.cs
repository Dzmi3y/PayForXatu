using Xamarin.Forms;
using PayForXatu.MobilApp.Controls;
using PayForXatu.MobilApp.Droid.CustomRenderers;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using PayForXatu.MobilApp.Droid;
using Android.Content;
using Android.Util;

[assembly: ExportRenderer(typeof(EntryPFX), typeof(EntryPFXRenderer))]
namespace PayForXatu.MobilApp.Droid.CustomRenderers
{
    [System.Obsolete]
    public class EntryPFXRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
                this.Control.SetTextCursorDrawable(Android.Graphics.Color.Argb(0, 0, 0, 0));
            }
        }
    }
}