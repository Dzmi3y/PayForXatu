
using PayForXatu.MAUIApp.Controls;
using PayForXatu.MAUIApp.Droid.CustomRenderers;
using Android.Text;

using Android.Content;
using Android.Util;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(EntryPFX), typeof(EntryPFXRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace PayForXatu.MAUIApp.Droid.CustomRenderers
{
    [System.Obsolete]
    public class EntryPFXRenderer : EntryRenderer
    {
        public EntryPFXRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                this.Control.SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
#pragma warning disable CA1416 // Validate platform compatibility
                this.Control.SetTextCursorDrawable(textCursorDrawable: Android.Graphics.Color.Argb(0, 0, 0, 0));
#pragma warning restore CA1416 // Validate platform compatibility
            }
        }
    }
}
