using CommunityToolkit.Maui.Views;
using Microsoft.VisualBasic;
using PayForXatu.MAUIApp.ViewModels;
using PayForXatu.MAUIApp.Views;
using System.Collections.ObjectModel;
using System.Net.Security;

namespace PayForXatu.MAUIApp.Controls;

public partial class PickerPFX : ContentView
{
	public PickerPFX()
	{
        InitializeComponent();
    }

    private void OnOpenModal()
    {
        var page = new PickerModalPFX(Collection,SelectedItem,
            (selectedItem) => { SelectedItem = selectedItem; });
        App.Current.MainPage.ShowPopup(page);

    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        OnOpenModal();
    }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
     "SelectedItem", typeof(string), typeof(string), "");

    public string SelectedItem
    {
        get => (string)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly BindableProperty CollectionProperty = BindableProperty.Create(
     "Collection", typeof(ObservableCollection<string>),
     typeof(ObservableCollection<string>), new ObservableCollection<string>());

    public ObservableCollection<string> Collection
    {
        get => (ObservableCollection<string>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}
