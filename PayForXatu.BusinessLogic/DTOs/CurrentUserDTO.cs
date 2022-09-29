using PayForXatu.Database.Models;

namespace PayForXatu.BusinessLogic.DTOs
{
    public class CurrentUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string UserId {get;set;} = string.Empty;
        public string FirebaseToken { get; set; } = string.Empty;
        public bool IsEmailProvider { get; set; }
        public UserSettings? UserSettings { get; set; }
    }
}
