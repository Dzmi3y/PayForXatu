using CommunityToolkit.Maui.Views;
using PayForXatu.MAUIApp.Resources;
using PayForXatu.MAUIApp.ViewModels;

namespace PayForXatu.MAUIApp.Views;

public partial class ForgotPasswordPage : ContentPage
{
    private ForgotPasswordPageViewModel vm;
    public ForgotPasswordPage()
    {
        Loaded += Page_Loaded;
        InitializeComponent();
    }

    private void Page_Loaded(object sender, EventArgs e)
    {
        vm = (ForgotPasswordPageViewModel)base.BindingContext;
        vm.OpenModal += OnOpenModal;
    }

    private void OnOpenModal()
    {
        var page = new ModalPage(async () => await OnCloseModal(), AppRes.ForgotPasswordText);
        this.ShowPopup(page);
    }

    private async Task OnCloseModal()
    {
        await vm.GoToLoginPageAsync();
    }
}
