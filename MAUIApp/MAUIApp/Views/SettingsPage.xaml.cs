using CommunityToolkit.Maui.Views;
using PayForXatu.MAUIApp.Controls;
using PayForXatu.MAUIApp.Resources;
using PayForXatu.MAUIApp.ViewModels;

namespace PayForXatu.MAUIApp.Views;

public partial class SettingsPage : BasePage
{
    private SettingsPageViewModel vm;
    public SettingsPage()
	{
        Loaded += Page_Loaded;
        InitializeComponent();
    }

    private void PickupGrid_Tapped(object sender, EventArgs e)
    {
        if (CurrencyPicker.IsFocused)
            CurrencyPicker.Unfocus();
        CurrencyPicker.Focus();
    }

    private void Page_Loaded(object sender, EventArgs e)
    {
        vm = (SettingsPageViewModel)base.BindingContext;
        vm.OpenModal += OnOpenModal;
        vm.OpenDeleteAccountModal += OnOpenDeleteAccountModal;
    }

    private void OnOpenModal(string message)
    {
        var page = new ModalPage(async () => await OnCloseModal(), message);
        this.ShowPopup(page);
    }

    private async Task OnCloseModal()
    {
    
    }


    private void OnOpenDeleteAccountModal(Action deleteAccount)
    {
        var page = new ConfirmModalPage(deleteAccount, AppRes.DeleteAccountModalText);

        this.ShowPopup(page);
    }
}
