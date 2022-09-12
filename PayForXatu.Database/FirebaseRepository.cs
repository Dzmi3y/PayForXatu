using Firebase.Database;
using Firebase.Database.Query;
using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.Database
{
    public class FirebaseRepository
    {
        const string auth = "e4bjVfPd4buchhYQSWx7VG72cTh9IwuRK0af0lYN";
        const string FirebaseDatabaseUrl = "https://testdbproj-866f2-default-rtdb.europe-west1.firebasedatabase.app/";
        readonly FirebaseClient firebaseClient;

        public FirebaseRepository()
        {
            FirebaseOptions fbOptions = new FirebaseOptions()
            {
                AuthTokenAsyncFactory = () => Task.FromResult(auth)
            };

            firebaseClient = new FirebaseClient(FirebaseDatabaseUrl, fbOptions);

        }

        public async Task Add<T>(T newChild)
        {
            string childName = GetChildNameByType(typeof(T));
            await firebaseClient
            .Child(childName)
            .PostAsync(newChild);
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetFirebaseObjectsAsync<T>()
        {
            string childName = GetChildNameByType(typeof(T));
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

        private async Task<FirebaseObject<T>?> GetFirebaseObjectAsync<T>(T updatedChild) where T : BaseEntity
        {
            var childs = await GetFirebaseObjectsAsync<T>();
            return childs.FirstOrDefault(
                x => ((x.Object as BaseEntity).Id == (updatedChild as BaseEntity).Id));
        }

        public async Task Update<T>(T updatedChild) where T : BaseEntity
        {
            string childName = GetChildNameByType(typeof(T));
            var child = await GetFirebaseObjectAsync(updatedChild);
            if (child != null)
            {
                await firebaseClient
                  .Child(childName)
                  .Child(child.Key)
                 .PutAsync(child.Object);
            }
        }

        private async Task<FirebaseObject<T>?> GetFirebaseObjectByIdAsync<T>(Guid id) where T : BaseEntity
        {
            var childs = await GetFirebaseObjectsAsync<T>();
            return childs.FirstOrDefault(
                x => ((x.Object as BaseEntity).Id == id));
        }

        public async Task DeleteAsync<T>(Guid id) where T : BaseEntity
        {
            string childName = GetChildNameByType(typeof(T));
            var child = await GetFirebaseObjectByIdAsync<T>(id);
            if (child != null)
            {
                await firebaseClient
                  .Child(childName)
                  .Child(child.Key)
                  .DeleteAsync();
            }
        }

        private string GetChildNameByType(Type t)
        {
            if (t == null)
                throw new Exception("Child shouldn't to be equal null");
            if (t == typeof(User))
                return "users";

            throw new Exception("Type is not found");
        }
    }


    public interface IFirebaseRepository
    {
        Task Add<T>(T newChild);
        Task<IReadOnlyCollection<FirebaseObject<T>>> GetFirebaseObjectsAsync<T>();
        Task<List<T>> GetListOfChildsAsync<T>();
        Task<FirebaseObject<T>?> GetFirebaseObjectAsync<T>(T updatedChild);
        Task Update<T>(T updatedChild);
        Task<FirebaseObject<T>?> GetFirebaseObjectByIdAsync<T>(Guid id);
        Task DeleteAsync<T>(Guid id);
        string GetChildNameByType(Type t);
    }
}
