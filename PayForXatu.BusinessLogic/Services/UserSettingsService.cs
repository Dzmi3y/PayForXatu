using PayForXatu.Database.Models;
using PayForXatu.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayForXatu.BusinessLogic.DTOs;
using Microsoft.Extensions.Configuration;
using Firebase.Auth;
using System.ComponentModel.DataAnnotations;

namespace PayForXatu.BusinessLogic.Services
{
    public class UserSettingsService: IUserSettingsService
    {
        private IFirebaseRepository _firebaseRepository;
        private string _webApiKey;
        private FirebaseAuthProvider _fbProvider;
        public UserSettingsService(IConfiguration config, IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository = firebaseRepository;
            _webApiKey = config.GetRequiredSection("WebApiKey").Value;
            _fbProvider = new FirebaseAuthProvider(new FirebaseConfig(_webApiKey));
        }

        public async Task<UserSettings?> GetUserSettingsByUserIdAsync(string userId)
        {
            List<UserSettings> userSettingsList = await _firebaseRepository.GetListOfChildsAsync<UserSettings>();
            var currentUserSettings = userSettingsList.FirstOrDefault(x => x.UserId == userId);
            return currentUserSettings;
        }

        public async Task<UserSettings> AddUserSettingsAsync(string userId)
        {
            var currenciesList = await _firebaseRepository.GetListOfChildsAsync<Currency>();
            var defaultCurrency = currenciesList.FirstOrDefault();
            var userSettings = new UserSettings()
            {
                Id = Guid.NewGuid(),
                Currency = defaultCurrency,
                UserId = userId
            };
            await _firebaseRepository.AddAsync<UserSettings>(userSettings);
            return userSettings;
        }

        public async Task UpdateUserSettingsAsync(UserSettings newUserSettings)
        {
            await _firebaseRepository.UpdateAsync(newUserSettings);
        }


        public async Task<ChangeEmailResponceDTO> ChangeEmail(string userId, string newEmail)
        {
            var responce = new ChangeEmailResponceDTO()
            {
                IsSuccess = true,
                Status = ChangeEmailResponseStatusEnum.Success
            };

            try
            {
                if (string.IsNullOrEmpty(newEmail))
                {
                    responce.IsSuccess = true;
                    responce.Status = ChangeEmailResponseStatusEnum.FieldIsNotFilledIn;
                    return responce;
                }

                if (!IsValidEmail(newEmail))
                {
                    responce.IsSuccess = false;
                    responce.Status = ChangeEmailResponseStatusEnum.IncorrectEmail;
                    return responce;
                }

                await _fbProvider.ChangeUserEmail(userId, newEmail);
                return responce;
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        responce.IsSuccess = false;
                        responce.Status = ChangeEmailResponseStatusEnum.UserAlreadyExists;
                        break;
                    default:
                        responce.IsSuccess = false;
                        responce.Status = ChangeEmailResponseStatusEnum.IncorrectEmail;
                        break;
                }
                return responce;
            }

        }

        public async Task<ChangePasswordResponceDTO> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var responce = new ChangePasswordResponceDTO()
            {
                IsSuccess = true,
                Status = ChangePasswordStatusEnum.Success
            };

            try
            {
                if (string.IsNullOrEmpty(changePasswordDTO.NewPassword) &&
                    string.IsNullOrEmpty(changePasswordDTO.ConfirmNewPassword))
                {
                    responce.IsSuccess = true;
                    responce.Status = ChangePasswordStatusEnum.FieldsAreNotFilledIn;
                    return responce;
                }

                if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmNewPassword)
                {
                    responce.IsSuccess = false;
                    responce.Status = ChangePasswordStatusEnum.PasswordsDoNotMatch;
                    return responce;
                }

                await _fbProvider.ChangeUserPassword(changePasswordDTO.UserId, changePasswordDTO.NewPassword);
                return responce;
            }
            catch (Exception ex)
            {
                responce.IsSuccess=false;
                responce.Message = ex.Message;
                responce.Status = ChangePasswordStatusEnum.Exception;
                return responce;
            }
        }

        public async Task DeleteAccountAsync(string firebaseToken)
        {
            //await _fbProvider.DeleteUserAsync(firebaseToken);
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

    public interface IUserSettingsService
    {
        Task<UserSettings?> GetUserSettingsByUserIdAsync(string userId);
        Task<UserSettings> AddUserSettingsAsync(string userId);
        Task UpdateUserSettingsAsync(UserSettings newUserSettings);
        Task<ChangePasswordResponceDTO> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<ChangeEmailResponceDTO> ChangeEmail(string userId, string newEmail);
        Task DeleteAccountAsync(string firebaseToken);
    }

}
