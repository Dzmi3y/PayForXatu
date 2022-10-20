using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Graphics.Font;

namespace PayForXatu.MAUIApp.Models.Chart
{
    public class GraphicsDrawable : IDrawable
    {
        ICanvas _canvas;
        Color _barColor;
        string _currency;
        RectF _dirtyRect;
        float _firstXCoordinat;
        int _space;
        float _maxBarHeight;
        float _maxValue;
        float _topBarSpace;
        float _width;
        List<BarInfo> _bars;

        public GraphicsDrawable(List<BarInfo> bars, Color barColor, string currency)
        {
            _bars = bars;
            _barColor = barColor;
            _currency = currency;
            _firstXCoordinat = 10;
            _space = 60;
            _maxBarHeight = 120;
            _topBarSpace = 10;
        }

        public Action<float> ChangeWidthAction { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _canvas = canvas;
            _dirtyRect = dirtyRect;

            var minWidth = (float)Application.Current.MainPage.Width - 35;
            _width = _firstXCoordinat + (_space * _bars.Count);
            _width = Math.Max(minWidth, _width);

            if (ChangeWidthAction != null)
            {
                ChangeWidthAction.Invoke(_width);
            }

            //Field
            Paint backgroundColor = Color.Parse("#241E1E").AsPaint();
            RectF linearRectangle = new RectF(0, 0, _width, 200);
            canvas.SetFillPaint(backgroundColor, linearRectangle);
            canvas.FillRoundedRectangle(linearRectangle, 0);

            //ox
            Paint oxColor = Color.Parse("#FFF").AsPaint();
            RectF oxRectangle = new RectF(10, 135, _width - 20, 1);
            canvas.SetFillPaint(oxColor, oxRectangle);
            canvas.FillRoundedRectangle(oxRectangle, 0);

            AddBars();
        }

        private void AddBars()
        {
            _maxValue = _bars.Max(b => b.Value);

            for (var i = 0; i < _bars.Count; i++)
            {
                float previousValue = (i == 0) ? 0 : _bars[i - 1].Value;
                AddBar(i, previousValue, _bars[i].Value, _bars[i].Date);
            }
        }

        private void AddBar(int currentBarNumber, float previousValue, float value, DateTime date)
        {
            float xStart = _firstXCoordinat + (_space * currentBarNumber);
            double differentValue = value - previousValue;
            string differentValueText;

            string amount = (string.IsNullOrEmpty(_currency)) ? $"{value}" : $"{value} {_currency}";

            Color differentValueColor = Color.Parse("#FFF");
            differentValueText = differentValue.ToString();

            if (differentValue > 0)
            {
                differentValueColor = Color.Parse("#0A7518");
                differentValueText = "+" + differentValueText;
            }

            if (differentValue < 0)
            {
                differentValueColor = Color.Parse("#600C0C");
            }

            float barHeight = (_maxBarHeight * value) / _maxValue;
            float barY = _topBarSpace + _maxBarHeight - barHeight;

            //bar
            Paint color = _barColor.AsPaint();
            RectF rectangle = new RectF(xStart + 15, barY, 20, barHeight);
            _canvas.SetFillPaint(color, rectangle);
            _canvas.FillRoundedRectangle(rectangle, 0);

            _canvas.Font = new Font("Arial", 800);
            //date
            _canvas.FontColor = Color.Parse("#FFF");
            _canvas.FontSize = 10;
            _canvas.DrawString(date.ToString("dd.MM.yyyy"), xStart, 140, 50, 10, HorizontalAlignment.Center, VerticalAlignment.Top);
            //time
            _canvas.FontColor = Color.Parse("#FFF");
            _canvas.FontSize = 10;
            _canvas.DrawString(date.ToString("HH:mm:ss"), xStart, 155, 50, 10, HorizontalAlignment.Center, VerticalAlignment.Top);
            //different
            _canvas.FontColor = differentValueColor;
            _canvas.DrawString(differentValueText, xStart, 170, 50, 10, HorizontalAlignment.Center, VerticalAlignment.Top);
            //ammount
            _canvas.FontColor = _barColor;
            _canvas.DrawString(amount, xStart, 185, 50, 10, HorizontalAlignment.Center, VerticalAlignment.Top);
        }
    }
}
