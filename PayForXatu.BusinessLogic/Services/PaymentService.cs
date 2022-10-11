using Firebase.Auth;
using PayForXatu.Database;
using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.Services
{
    public class PaymentService: IPaymentService
    {
        private IFirebaseRepository _firebaseRepository;
        public PaymentService(IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository=firebaseRepository;
        }

        public async Task<List<SavedPayment>> GetSavedPaymentsAsync(string userId)
        {
            var allPayments = await _firebaseRepository.GetListOfChildsAsync<SavedPayment>();
            var result = allPayments.Where(x => x.UserId == userId).ToList();

            return result ?? new List<SavedPayment>();
        }

        public async Task<SavedPayment?> GetSavedPaymentByIdAsync(Guid paymentId)
        {
            var firebaseObject = await _firebaseRepository.GetFirebaseObjectByIdAsync<SavedPayment>(paymentId);
            return firebaseObject?.Object;
        }

        public async Task AddPaymentAsync(SavedPayment newPaymnet)
        {
            await _firebaseRepository.AddAsync<SavedPayment>(newPaymnet);
        }

        public async Task RemovePaymentAsync(Guid paymentId)
        {
            await _firebaseRepository.DeleteAsync<SavedPayment>(paymentId);
        }

        public async Task UpdatePaymentAsync(SavedPayment updatedPaymnet)
        {
            await _firebaseRepository.UpdateAsync<SavedPayment>(updatedPaymnet);
        }
    }

    public interface IPaymentService
    {
        Task<List<SavedPayment>> GetSavedPaymentsAsync(string userId);
        Task AddPaymentAsync(SavedPayment newPaymnet);
        Task RemovePaymentAsync(Guid paymentId);
        Task UpdatePaymentAsync(SavedPayment updatedPaymnet);
        Task<SavedPayment?> GetSavedPaymentByIdAsync(Guid paymentId);
    }
}
