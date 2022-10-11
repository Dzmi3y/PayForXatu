using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database.Models
{
    public class SavedPayment:BaseEntity
    {
        public SavedPayment()
        {
            Counters = new List<string>();
        }
        public string UserId { get; set; } = string.Empty;
        public string PaymentName { get; set; } =string.Empty;
        public List<string> Counters { get; set; }

    }
}
