namespace PayForXatu.BusinessLogic.DTOs
{
    public class LogInResponseDTO
    {
        public CurrentUserDTO? CurrentUser { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public LogInResponseStatusEnum Status { get; set; }
    }

    public enum LogInResponseStatusEnum
    {
        GoogleAccountNotFound,
        EmailNotVerified,
        InvalidEmailOrPassword,
        Exception,
        Success
    }
}
