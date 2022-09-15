using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using PayForXatu.BusinessLogic.DTOs;

namespace PayForXatu.BusinessLogic.Services
{
    public class LogInService : ILogInService
    {
        string _clientId;
        string _webApiKey;
        IGoogleManager _googleManager;
        public LogInService(IConfiguration config, IGoogleManager googleManager)
        {
            _googleManager = googleManager;
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

                return logInResponseDTO;
            }
            catch (Exception ex)
            {
                logInResponseDTO.IsSuccess = false;
                logInResponseDTO.Status = LogInResponseStatusEnum.InvalidEmailOrPassword;

                return logInResponseDTO;
            }
        }

        public LogInResponseDTO LoginWithGoogleAuth(Action<GoogleUserDTO, string> OnGoogleLoginComplete)
        {
            var logInResponseDTO = new LogInResponseDTO();
            logInResponseDTO.IsSuccess = true;
            logInResponseDTO.Status = LogInResponseStatusEnum.Success;

            try
            {
                _googleManager.Login(_clientId, OnGoogleLoginComplete);
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
        LogInResponseDTO LoginWithGoogleAuth(Action<GoogleUserDTO, string> OnGoogleLoginComplete);
        Task<LogInResponseDTO> AddGoogleAccauntToFirebaseAsync(GoogleUserDTO googleUserDTO);
    }

}
