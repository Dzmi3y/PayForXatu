using Android.Views;
using Android.Widget;
using CommunityToolkit.Maui.Views;
using DryIoc.FastExpressionCompiler.LightExpression;
using Microsoft.Maui;
using PayForXatu.MAUIApp.Resources;
using PayForXatu.MAUIApp.ViewModels;

namespace PayForXatu.MAUIApp.Views;

public partial class HomePage : BasePage
{

    private HomePageViewModel vm;
    public HomePage()
    {
        Loaded += Page_Loaded;
        InitializeComponent();
    }

    private void Page_Loaded(object sender, EventArgs e)
    {
        vm = (HomePageViewModel)base.BindingContext;
        vm.OpenSavePaymentDataModal += OnSavePaymentDataModal;
        vm.CloseEditGridModal += OnCloseEditGridModal;
        vm.RemovePaymentDataModal += OnRemovePaymentDataModal;
        vm.SaveChangesPaymentDataModal += OnSaveChangesPaymentDataModal;

    }

    private void OnSavePaymentDataModal(Action savePaymentData)
    {
        var page = new ConfirmModalPage(savePaymentData, AppRes.SavePaymentData);

        this.ShowPopup(page);
    }

    private void OnCloseEditGridModal(Action closeEditGrid)
    {
        var page = new ConfirmModalPage(closeEditGrid, AppRes.CloseEditGrid);

        this.ShowPopup(page);
    }
    private void OnRemovePaymentDataModal(Action removePaymentData)
    {
        var page = new ConfirmModalPage(removePaymentData, AppRes.RemovePaymentData);

        this.ShowPopup(page);
    }
    private void OnSaveChangesPaymentDataModal(Action saveChangesPaymentData)
    {
        var page = new ConfirmModalPage(saveChangesPaymentData, AppRes.SaveChangesPaymentData);

        this.ShowPopup(page);
    }

    private void Unfocus(object sender, EventArgs e)
    {
        TitleLabel.Focus();
    }
}
