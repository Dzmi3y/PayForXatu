using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using PayForXatu.BusinessLogic.DTOs;

namespace PayForXatu.BusinessLogic.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        string _webApiKey;

        public ForgotPasswordService(IConfiguration config)
        {
            _webApiKey = config.GetRequiredSection("WebApiKey").Value;
        }

        public async Task<ForgotPasswordDTO> ForgotPasswordAsync(string email)
        {

            var forgotPasswordDTO = new ForgotPasswordDTO();
            forgotPasswordDTO.IsSuccess = true;
            forgotPasswordDTO.Status = ForgotPasswordStatusEnum.Success;
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    forgotPasswordDTO.IsSuccess = false;
                    forgotPasswordDTO.Status = ForgotPasswordStatusEnum.EmailIsEmpty;
                    return forgotPasswordDTO;
                }

                if (!IsValidEmail(email))
                {
                    forgotPasswordDTO.IsSuccess = false;
                    forgotPasswordDTO.Status = ForgotPasswordStatusEnum.IncorrectEmail;
                    return forgotPasswordDTO;
                }

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApiKey));
                await authProvider.SendPasswordResetEmailAsync(email);
                return forgotPasswordDTO;
            }
            catch (Exception ex)
            {
                forgotPasswordDTO.IsSuccess = false;
                forgotPasswordDTO.Status = ForgotPasswordStatusEnum.Exception;
                forgotPasswordDTO.Message = ex.Message;

                return forgotPasswordDTO;
            }
        }

        private static bool IsValidEmail(string email)
        {
            bool emailIsCorrect = true;
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                emailIsCorrect = false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != trimmedEmail)
                {
                    emailIsCorrect = false;
                }
            }
            catch
            {
                emailIsCorrect = false;
            }

            return emailIsCorrect;
        }
    }

    public interface IForgotPasswordService
    {
        Task<ForgotPasswordDTO> ForgotPasswordAsync(string email);
    }
}











