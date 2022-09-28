namespace PayForXatu.BusinessLogic.DTOs
{
    public class ChangePasswordResponceDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ChangePasswordStatusEnum Status { get; set; }
    }

    public enum ChangePasswordStatusEnum
    {
        FieldsAreNotFilledIn,
        PasswordsDoNotMatch,
        Exception,
        Success
    }
}
