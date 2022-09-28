using PayForXatu.Database.Models;

namespace PayForXatu.BusinessLogic.DTOs
{
    public class CurrentUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string UserId {get;set;} = string.Empty;
        public UserSettings? UserSettings { get; set; }
    }
}
