using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.MAUIApp.Models.Chart
{
    public class BarInfo
    {
        public BarInfo(float value, DateTime date)
        {
            Value = value;
            Date = date;
        }
        public float Value { get; set; }
        public DateTime Date { get; set; }
    }
}
