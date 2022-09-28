using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database
{
    public class FirebaseRepository: IFirebaseRepository
    {
        readonly FirebaseClient firebaseClient;

        public FirebaseRepository(IConfiguration config)
        {
            
            var firebaseDatabaseToken = config.GetRequiredSection("FirebaseDatabaseToken").Value;
            var firebaseDatabaseUrl = config.GetRequiredSection("FirebaseDatabaseUrl").Value;

            FirebaseOptions fbOptions = new FirebaseOptions()
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseDatabaseToken)
            };

            firebaseClient = new FirebaseClient(firebaseDatabaseUrl, fbOptions);

        }

        public async Task AddAsync<T>(T newChild)
        {
            string childName = SetUpDbModels.GetChildNameByType(typeof(T));
            await firebaseClient
            .Child(childName)
            .PostAsync(newChild);
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetFirebaseObjectsAsync<T>()
        {
            string childName = SetUpDbModels.GetChildNameByType(typeof(T));
            return await firebaseClient
          .Child(childName)
          .OnceAsync<T>();
        }

        public async Task<List<T>> GetListOfChildsAsync<T>()
        {
            var childs = await GetFirebaseObjectsAsync<T>();

            List<T> result = new List<T>();
            result.AddRange(childs.Select(c => c.Object).ToList<T>());
            return result;
        }

        public async Task<FirebaseObject<T>?> GetFirebaseObjectAsync<T>(T updatedChild) where T : BaseEntity
        {
            var childs = await GetFirebaseObjectsAsync<T>();
            return childs.FirstOrDefault(
                x => ((x.Object as BaseEntity).Id == (updatedChild as BaseEntity).Id));
        }

        public async Task UpdateAsync<T>(T updatedChild) where T : BaseEntity
        {
            string childName = SetUpDbModels.GetChildNameByType(typeof(T));
            var child = await GetFirebaseObjectAsync(updatedChild);
            if (child != null)
            {
                await firebaseClient
                  .Child(childName)
                  .Child(child.Key)
                 .PutAsync(updatedChild);
            }
        }

        public async Task<FirebaseObject<T>?> GetFirebaseObjectByIdAsync<T>(Guid id) where T : BaseEntity
        {
            var childs = await GetFirebaseObjectsAsync<T>();
            return childs.FirstOrDefault(
                x => ((x.Object as BaseEntity).Id == id));
        }

        public async Task DeleteAsync<T>(Guid id) where T : BaseEntity
        {
            string childName = SetUpDbModels.GetChildNameByType(typeof(T));
            var child = await GetFirebaseObjectByIdAsync<T>(id);
            if (child != null)
            {
                await firebaseClient
                  .Child(childName)
                  .Child(child.Key)
                  .DeleteAsync();
            }
        }
    }


    public interface IFirebaseRepository
    {
        Task AddAsync<T>(T newChild);
        Task<IReadOnlyCollection<FirebaseObject<T>>> GetFirebaseObjectsAsync<T>();
        Task<List<T>> GetListOfChildsAsync<T>();
        Task<FirebaseObject<T>?> GetFirebaseObjectAsync<T>(T updatedChild) where T : BaseEntity;
        Task UpdateAsync<T>(T updatedChild) where T : BaseEntity;
        Task<FirebaseObject<T>?> GetFirebaseObjectByIdAsync<T>(Guid id) where T : BaseEntity;
        Task DeleteAsync<T>(Guid id) where T : BaseEntity;
    }
}
