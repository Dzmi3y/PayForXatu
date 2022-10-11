using CommunityToolkit.Maui.Views;
using PayForXatu.MAUIApp.Resources;
using PayForXatu.MAUIApp.ViewModels;

namespace PayForXatu.MAUIApp.Views;

public class BasePage : ContentPage
{
    private ViewModelBase vm;
    public BasePage()
    {
       
        Loaded += Page_Loaded;
    }        

    private void Page_Loaded(object sender, EventArgs e)
    {
        vm = (ViewModelBase)base.BindingContext;
        vm.OpenLogoutModal += OnOpenLogoutModal;

    }

    private void OnOpenLogoutModal(Action logout)
    {
        var page = new ConfirmModalPage(logout, AppRes.LogoutModalText);

        this.ShowPopup(page);
    }
}

