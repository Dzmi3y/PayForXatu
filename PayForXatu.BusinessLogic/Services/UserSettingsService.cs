using PayForXatu.Database.Models;
using PayForXatu.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.Services
{
    public class UserSettingsService: IUserSettingsService
    {
        private IFirebaseRepository _firebaseRepository;
        public UserSettingsService(IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository = firebaseRepository;
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
    }

    public interface IUserSettingsService
    {
        Task<UserSettings?> GetUserSettingsByUserIdAsync(string userId);
        Task<UserSettings> AddUserSettingsAsync(string userId);
        Task UpdateUserSettingsAsync(UserSettings newUserSettings);
    }

}
