using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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
        private IFirebaseRepository _firebaseRepository;
        public SignUpService(IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository = firebaseRepository;
        }

        public async Task<ServiceResponseDTO> SignUpAsync(SignUpUserDTO signUpUser)
        {
            var serviceResponseDTO = new ServiceResponseDTO();
            serviceResponseDTO.IsSuccess = true;
            try
            {
                if (string.IsNullOrEmpty(signUpUser.Password) ||
                    string.IsNullOrEmpty(signUpUser.ConfirmPassword) ||
                    string.IsNullOrEmpty(signUpUser.Email))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "All fields must be filled in";
                    return serviceResponseDTO;
                }

                if (!IsValidEmail(signUpUser))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "Incorrect email";
                    return serviceResponseDTO;
                }

                if (signUpUser.Password != signUpUser.ConfirmPassword)
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "Passwords don't match";
                    return serviceResponseDTO;
                }

                var userList = await _firebaseRepository.GetListOfChildsAsync<User>();
                if (userList.Any(u => u.Email == signUpUser.Email))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "User with this email already exists";
                    return serviceResponseDTO;
                }

                var passwordHasher = new PasswordHasher<User>();

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    IsEmailConfirmed = false,
                    Email = signUpUser.Email
                };
                user.PasswordHash = passwordHasher.HashPassword(user, signUpUser.Password);
                await _firebaseRepository.AddAsync(user);

                return serviceResponseDTO;
            }
            catch (Exception ex)
            {
                serviceResponseDTO.IsSuccess=false;
                serviceResponseDTO.Message = ex.Message;
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
        Task<ServiceResponseDTO> SignUpAsync(SignUpUserDTO signUpUser);
    }
}
