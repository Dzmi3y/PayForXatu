using PayForXatu.MobilApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayForXatu.MobilApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private ForgotPasswordViewModel vm;
        public ForgotPasswordPage()
        {
            InitializeComponent();

           vm = (ForgotPasswordViewModel)base.BindingContext;
           vm.OpenModal += OnOpenModal;
        }

        private void OnOpenModal()
        {
            var page = new ModalPage( async()=>await OnCloseModal(), Resource.ForgotPasswordText);
            Navigation.PushModalAsync(page, true);
        }

        private async Task OnCloseModal()
        {
            await Navigation.PopModalAsync();
            await vm.GoToLoginPageAsync();
        }
    }
}