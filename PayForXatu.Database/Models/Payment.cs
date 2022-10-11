using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database.Models
{
    public class Payment : BaseEntity
    {
        public Payment()
        { 
            Counters = new List<Counter>();
        }
        public string UserId { get; set; } = string.Empty;
        public string PaymentName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<Counter> Counters { get; set; }
        public double Amount { get; set; }
    }

    public class Counter
    {
        public string CounterName { get; set; } = string.Empty;
        public double CounterValue { get; set; }
    }
}
