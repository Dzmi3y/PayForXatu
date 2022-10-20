using Android.Graphics.Fonts;
using Java.Util;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Maui.Graphics;
using PayForXatu.BusinessLogic.Services;
using PayForXatu.MAUIApp.Models;
using PayForXatu.MAUIApp.Models.Chart;
using PayForXatu.MAUIApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Graphics.Font;

namespace PayForXatu.MAUIApp.ViewModels
{
    public class AnalyticsPageViewModel : ViewModelBase
    {
        private ObservableCollection<string> _paymentsNamesList;
        private DateTime _endDate;
        private DateTime _startDate;
        private string _selectedItem;
        private GraphicsDrawable _graphics;
        private double _graphicsWidth;

        public AnalyticsPageViewModel(INavigationService navigationService, IMemoryCache memoryCache,
            ICurrencyService currencyService)
            : base(navigationService, memoryCache, currencyService)
        {
            Title = "Analytics";
            StartDate = DateTime.Now.AddDays(-2);
            EndDate = DateTime.Now;
            PaymentsNamesList = new ObservableCollection<string>();
            GraphicsWidth = 500;

            for (var i = 0; i < 50; i++)
            {
                PaymentsNamesList.Add($"p{i}");
            }

            SelectedItem = PaymentsNamesList.FirstOrDefault();
            var _bars = new List<BarInfo>();
            _bars.Add(new BarInfo(11, DateTime.Now));
            _bars.Add(new BarInfo(20, DateTime.Now));
            _bars.Add(new BarInfo(15, DateTime.Now));
            UpdateGraphics(_bars);
        }

        public void UpdateGraphics(List<BarInfo> bars)
        {
            Graphics = new GraphicsDrawable(bars, Color.Parse("#345995"),
            "BYR") //CurrentUser.UserSettings.Currency.Name
            {
                ChangeWidthAction = (width) =>
                {
                    GraphicsWidth = width;
                }
            };
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableCollection<string> PaymentsNamesList
        {
            get { return _paymentsNamesList; }
            set { SetProperty(ref _paymentsNamesList, value); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
                if(value>DateTime.Now)
                {
                    var _bars = new List<BarInfo>();
                    _bars.Add(new BarInfo(11, DateTime.Now));
                    _bars.Add(new BarInfo(20, DateTime.Now));
                    _bars.Add(new BarInfo(15, DateTime.Now));
                    _bars.Add(new BarInfo(11, DateTime.Now));
                    _bars.Add(new BarInfo(20, DateTime.Now));
                    _bars.Add(new BarInfo(15, DateTime.Now));
                    _bars.Add(new BarInfo(11, DateTime.Now));
                    _bars.Add(new BarInfo(20, DateTime.Now));
                    _bars.Add(new BarInfo(15, DateTime.Now));
                    _bars.Add(new BarInfo(11, DateTime.Now));
                    _bars.Add(new BarInfo(20, DateTime.Now));
                    _bars.Add(new BarInfo(15, DateTime.Now));

                    UpdateGraphics(_bars);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set{SetProperty(ref _endDate, value);}
        }

        public GraphicsDrawable Graphics
        {
            get { return _graphics; }
            set { SetProperty(ref _graphics, value); }
        }
        
        public double GraphicsWidth
        {
            get { return _graphicsWidth; }
            set { SetProperty(ref _graphicsWidth, value); }
        }
    }
}
