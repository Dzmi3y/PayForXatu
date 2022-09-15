using PayForXatu.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic
{
    public interface IGoogleManager
    {
        void Login(string clientId, Action<GoogleUserDTO, string> OnLoginComplete);

        void Logout();
    }
}
