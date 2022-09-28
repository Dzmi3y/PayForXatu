using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database
{
    public static class SetUpDbModels
    {
        public static string GetChildNameByType(Type t)
        {
            if (t == null)
                throw new Exception("Child shouldn't to be equal null");
            if (t == typeof(UserSettings))
                return "user_settings";
            if (t == typeof(Currency))
                return "currencies";

            throw new Exception("Type is not found");
        }
    }
}
