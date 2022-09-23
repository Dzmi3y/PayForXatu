using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using Xamarin.Google.Crypto.Tink.Prf;

namespace PayForXatu.MAUIApp.Controls;

public partial class MenuPFX : ContentView
{
    private string _urlFlashlightOff = "flashlight_off.png";
    private string _urlFlashlightOn = "flashlight_on.png";
    public ObservableCollection<MenuItem> MenuItems { get; set; }

    public MenuPFX()
    {
        MenuItems = new ObservableCollection<MenuItem>();
        InitializeComponent();
        MenuIsOpen = false;
        FlashlightIsOn = false;
        Loaded += MenuPFX_Loaded;
        RefreshMainStackBackground();
        FillMenuItems();
        
    }

    private void MenuPFX_Loaded(object sender, EventArgs e)
    {
        SetSelectedItem();
        FlashlightIcon = (FlashlightIsOn) ? _urlFlashlightOn : _urlFlashlightOff;
    }

    private void SetSelectedItem()
    {
        if (!string.IsNullOrEmpty(Title))
        {
            var item = MenuItems.FirstOrDefault(x => x.Title == Title);
            if (item != null)
            {
                item.IsSelected = true;

            }
        };
    }

    private void FillMenuItems()
    {
        MenuItems.Add(new MenuItem()
        {
            Title = "Home",
            ImgUrl = "home.png",
            ImgUrlSelected = "home_selected.png",
            UrlPage = "HomePage"
        });
        MenuItems.Add(new MenuItem()
        {
            Title = "History",
            ImgUrl = "history.png",
            ImgUrlSelected = "history_selected.png",
            UrlPage = "HistoryPage"
        });
        MenuItems.Add(new MenuItem()
        {
            Title = "Analytics",
            ImgUrl = "analytics.png",
            ImgUrlSelected = "analytics_selected.png",
            UrlPage = "AnalyticsPage"
        });
        MenuItems.Add(new MenuItem()
        {
            Title = "Settings",
            ImgUrl = "settings.png",
            ImgUrlSelected = "settings_selected.png",
            UrlPage = "SettingsPage"
        });
        MenuItems.Add(new MenuItem()
        {
            Title = "Log Out",
            ImgUrl = "logout.png",
            ImgUrlSelected = "logout.png",
            UrlPage = "LoginPage"
        });
    }

    private void RefreshMainStackBackground()
    {
        var vgb = new LinearGradientBrush();
        vgb.EndPoint = new Point(0, 1);

        var gs1 = new GradientStop();
        gs1.Color = Color.FromHex("#333");
        gs1.Offset = (float)0.3;

        var gs2 = new GradientStop();
        gs2.Color = Color.FromHex("#000");
        gs2.Offset = (float)1.0;

        vgb.GradientStops.Add(gs1);
        vgb.GradientStops.Add(gs2);

        MainStack.Background = vgb;
    }


    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
    "Title", typeof(string), typeof(string), "");

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty FlashlightIsOnProperty = BindableProperty.Create(
     "FlashlightIsOn", typeof(bool), typeof(bool), false);

    public bool FlashlightIsOn
    {
        get { return (bool)GetValue(FlashlightIsOnProperty); }
        set
        {
            FlashlightIcon = (value) ? _urlFlashlightOn : _urlFlashlightOff;
            SetValue(FlashlightIsOnProperty, value);
        }
    }

    public static readonly BindableProperty MenuIsOpenProperty = BindableProperty.Create(
     "MenuIsOpen", typeof(bool), typeof(bool), false);

    public bool MenuIsOpen
    {
        get => (bool)GetValue(MenuIsOpenProperty);
        set => SetValue(MenuIsOpenProperty, value);
    }


    public static readonly BindableProperty FlashlightIconProperty = BindableProperty.Create(
     "FlashlightIcon", typeof(string), typeof(string), "");

    public string FlashlightIcon
    {
        get => (string)GetValue(FlashlightIconProperty);
        set => SetValue(FlashlightIconProperty, value);
    }

    private void Flashlight_Tapped(object sender, EventArgs e)
    {
        FlashlightIsOn = !FlashlightIsOn;
    }

    private void Burger_Tapped(object sender, EventArgs e)
    {
        MenuIsOpen = !MenuIsOpen;
        RefreshMainStackBackground();
    }

    private void ItemMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var item = (MenuItem)e.Item;
        if (TapMenuCommand != null)
            if (TapMenuCommand.CanExecute(item.UrlPage))
                TapMenuCommand.Execute(item.UrlPage);
    }

    public static readonly BindableProperty TapMenuCommandProperty = BindableProperty.Create(
     "TapMenuCommand", typeof(ICommand), typeof(ICommand), null);

    public ICommand TapMenuCommand
    {
        get => (ICommand)GetValue(TapMenuCommandProperty);
        set => SetValue(TapMenuCommandProperty, value);
    }

}


public class MenuItem: BindableBase
{
    private bool _isSelected;

    public string ImgUrl { get; set; }
    public string ImgUrlSelected { get; set; }
    public string Title { get; set; }
    public string UrlPage { get; set; }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetProperty(ref _isSelected, value); }
    }
}


