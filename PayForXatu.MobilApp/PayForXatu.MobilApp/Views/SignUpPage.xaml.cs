using PayForXatu.MobilApp.ViewModels;
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
    public partial class SignUpPage : ContentPage
    {
        private SignUpViewModel vm;
        public SignUpPage()
        {
            InitializeComponent();
            vm = (SignUpViewModel)base.BindingContext;
            vm.OpenModal += OnOpenModal;
        }

        private void OnOpenModal()
        {
            var page = new ModalPage(async () => await OnCloseModal(), Resource.SignUpText);
            Navigation.PushModalAsync(page, true);
        }

        private async Task OnCloseModal()
        {
            await Navigation.PopModalAsync();
            await vm.GoToLoginPageAsync();
        }
    }
}