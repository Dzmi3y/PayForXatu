using Firebase.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayForXatu.BusinessLogic.DTOs;
using PayForXatu.Database;
using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.Services
{
    public class SignUpService : ISignUpService
    {
        private string _webApiKey;
        public SignUpService(IConfiguration config)
        {
            _webApiKey = config.GetRequiredSection("WebApiKey").Value;
        }

        public async Task<SignUpResponseDTO> SignUpAsync(SignUpUserDTO signUpUser)
        {
            var serviceResponseDTO = new SignUpResponseDTO();
            serviceResponseDTO.IsSuccess = true;
            serviceResponseDTO.Status = SignUpResponseStatusEnum.Success;
            try
            {
                if (string.IsNullOrEmpty(signUpUser.Password) ||
                    string.IsNullOrEmpty(signUpUser.ConfirmPassword) ||
                    string.IsNullOrEmpty(signUpUser.Email))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Status = SignUpResponseStatusEnum.FieldsAreNotFilledIn;
                    return serviceResponseDTO;
                }

                if (!IsValidEmail(signUpUser))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Status = SignUpResponseStatusEnum.IncorrectEmail;
                    return serviceResponseDTO;
                }

                if (signUpUser.Password != signUpUser.ConfirmPassword)
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Status = SignUpResponseStatusEnum.PasswordsDoNotMatch;
                    return serviceResponseDTO;
                }

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(signUpUser.Email, signUpUser.Password);
                await authProvider.SendEmailVerificationAsync(auth);
                return serviceResponseDTO;
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        serviceResponseDTO.IsSuccess = false;
                        serviceResponseDTO.Status = SignUpResponseStatusEnum.UserAlreadyExists;
                        break;
                    default:
                        serviceResponseDTO.IsSuccess = false;
                        serviceResponseDTO.Status = SignUpResponseStatusEnum.IncorrectEmail;
                        break;
                }

                return serviceResponseDTO;
            }
        }

        private static bool IsValidEmail(SignUpUserDTO signUpUser)
        {
            bool emailIsCorrect = true;
            var trimmedEmail = signUpUser.Email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                emailIsCorrect = false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(signUpUser.Email);
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

    public interface ISignUpService
    {
        Task<SignUpResponseDTO> SignUpAsync(SignUpUserDTO signUpUser);
    }
}
