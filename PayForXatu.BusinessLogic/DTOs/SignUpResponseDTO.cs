using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.DTOs
{
    public class SignUpResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public SignUpResponseStatusEnum Status { get; set; }
    }

    public enum SignUpResponseStatusEnum
    {
        FieldsAreNotFilledIn,
        IncorrectEmail,
        PasswordsDoNotMatch,
        UserAlreadyExists,
        Exception,
        Success
    }
}
