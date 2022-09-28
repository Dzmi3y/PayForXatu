namespace PayForXatu.BusinessLogic.DTOs
{
    public class ChangeEmailResponceDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ChangeEmailResponseStatusEnum Status { get; set; }
    }

    public enum ChangeEmailResponseStatusEnum
    {
        FieldIsNotFilledIn,
        IncorrectEmail,
        UserAlreadyExists,
        Exception,
        Success
    }
}
