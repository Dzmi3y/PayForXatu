using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.Database.Models;

namespace PayForXatu.BusinessLogic.Services
{
    public class LogInService : ILogInService
    {
        string _clientId;
        string _webApiKey;
        IGoogleManager _googleManager;
        IUserSettingsService _userSettingsService;
        public LogInService(IConfiguration config, IGoogleManager googleManager, 
            IUserSettingsService userSettingsService)
        {
            _googleManager = googleManager;
            _userSettingsService = userSettingsService;
            _webApiKey = config.GetRequiredSection("WebApiKey").Value;
            _clientId = config.GetRequiredSection("ClientId").Value;
        }

        public async Task<LogInResponseDTO> LoginWithEmailAndPasswordAsync(string email, string password)
        {
            var logInResponseDTO = new LogInResponseDTO();
            logInResponseDTO.IsSuccess = true;
            logInResponseDTO.Status = LogInResponseStatusEnum.Success;

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApiKey));
                var firebaseAccountLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var user = firebaseAccountLink.User;

                if (user == null)
                {
                    logInResponseDTO.IsSuccess = false;
                    logInResponseDTO.Status = LogInResponseStatusEnum.InvalidEmailOrPassword;

                    return logInResponseDTO;
                }

                if (!user.IsEmailVerified)
                {
                    logInResponseDTO.IsSuccess = false;
                    logInResponseDTO.Status = LogInResponseStatusEnum.EmailNotVerified;
                }
                var userSettings = await GetUserSettingsByIdAsync(user.LocalId);
                logInResponseDTO.CurrentUser = new CurrentUserDTO()
                {
                    Email = user.Email,
                    UserId = user.LocalId,
                    UserSettings = userSettings
                };

                return logInResponseDTO;
            }
            catch (Exception ex)
            {
                logInResponseDTO.IsSuccess = false;
                logInResponseDTO.Status = LogInResponseStatusEnum.InvalidEmailOrPassword;

                return logInResponseDTO;
            }
        }

        private async Task<UserSettings> GetUserSettingsByIdAsync(string userId)
        {
           var userSettings = await _userSettingsService.GetUserSettingsByUserIdAsync(userId);

            if (userSettings == null)
            {
                userSettings = await _userSettingsService.AddUserSettingsAsync(userId);
            }

            return userSettings;
        }

        public void LoginWithGoogleAuth(Action<GoogleUserDTO, string> OnGoogleLoginComplete)
        {
                _googleManager.Login(_clientId, OnGoogleLoginComplete);
        }

        public async Task<LogInResponseDTO> AddGoogleAccauntToFirebaseAsync(GoogleUserDTO googleUserDTO)
        {
            var logInResponseDTO = new LogInResponseDTO();
            logInResponseDTO.IsSuccess = true;
            logInResponseDTO.Status = LogInResponseStatusEnum.Success;

            try
            {
                if (string.IsNullOrEmpty(googleUserDTO.Token))
                {
                    logInResponseDTO.IsSuccess = false;
                    logInResponseDTO.Status = LogInResponseStatusEnum.GoogleAccountNotFound;
                    return logInResponseDTO;
                }

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApiKey));
                var firebaseAccountLink = await authProvider.SignInWithGoogleIdTokenAsync(googleUserDTO.Token);
                var user = firebaseAccountLink.User;

                var userSettings = await GetUserSettingsByIdAsync(user.LocalId);
                logInResponseDTO.CurrentUser = new CurrentUserDTO()
                {
                    Email = user.Email,
                    UserId = user.LocalId,
                    UserSettings = userSettings
                };

                return logInResponseDTO;
            }
            catch (Exception ex)
            {
                logInResponseDTO.IsSuccess = false;
                logInResponseDTO.Status = LogInResponseStatusEnum.Exception;
                logInResponseDTO.Message = ex.Message;

                return logInResponseDTO;
            }
        }
    }

    public interface ILogInService
    {
        Task<LogInResponseDTO> LoginWithEmailAndPasswordAsync(string email, string password);
        void LoginWithGoogleAuth(Action<GoogleUserDTO, string> OnGoogleLoginComplete);
        Task<LogInResponseDTO> AddGoogleAccauntToFirebaseAsync(GoogleUserDTO googleUserDTO);
    }

}
