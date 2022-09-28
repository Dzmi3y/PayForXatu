using Android.Content;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using PayForXatu.MAUIApp.Controls;
using PayForXatu.MAUIApp.Platforms.Android.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(PickerPFX), typeof(PickerPFXRenderer))]
namespace PayForXatu.MAUIApp.Platforms.Android.CustomRenderers
{
    public class PickerPFXRenderer: PickerRenderer
    {
        public PickerPFXRenderer(Context context) : base(context)
        {
           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}
