using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace PayForXatu.MAUIApp.Controls;

public partial class PickerModalPFX : Popup
{
    private Action<string> _select;
    public ObservableCollection<string> PaymnetsNameList { get; set; }
    public PickerModalPFX(ObservableCollection<string> collection,string oldSelectedItem, Action<string> select)
    {
        InitializeComponent();
        _select = select;
        BindingContext = this;
        PaymnetsNameList = collection;

        PickerCollection.ItemsSource = PaymnetsNameList;
        PickerCollection.SelectedItem = oldSelectedItem;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Close();
    }

    private void PickerCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = ((string)e.CurrentSelection[0]);
        if (_select != null)
            _select.Invoke(selectedItem);

        try
        {
            Close();
        }
        catch
        { }
    }

    private void SearchEntryPFX_TextChanged(object sender, TextChangedEventArgs e)
    {
       var searchKey = e.NewTextValue;

        PickerCollection.ItemsSource = PaymnetsNameList
           .Where(x => x.Contains(searchKey))
           .ToObservableCollection();
    }
}
