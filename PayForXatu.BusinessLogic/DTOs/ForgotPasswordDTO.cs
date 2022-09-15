
namespace PayForXatu.BusinessLogic.DTOs
{
    public class ForgotPasswordDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ForgotPasswordStatusEnum Status { get; set; }
    }

    public enum ForgotPasswordStatusEnum
    {
        EmailIsEmpty,
        IncorrectEmail,
        Exception,
        Success
    }
}
