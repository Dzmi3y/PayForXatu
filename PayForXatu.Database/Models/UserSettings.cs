using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database.Models
{
    public class UserSettings:BaseEntity
    {
        public string UserId { get; set; }
        public Currency Currency { get; set; }
    }
}
