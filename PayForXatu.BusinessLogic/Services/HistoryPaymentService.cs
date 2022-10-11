using PayForXatu.Database.Models;
using PayForXatu.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.Services
{
    public class HistoryPaymentService: IHistoryPaymentService
    {
        private IFirebaseRepository _firebaseRepository;
        public HistoryPaymentService(IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository = firebaseRepository;
        }

        public async Task<List<Payment>> GetPaymentsAsync(string userId)
        {
            var allPayments = await _firebaseRepository.GetListOfChildsAsync<Payment>();
            var result = allPayments.Where(x => x.UserId == userId).ToList();
             
            return result ?? new List<Payment>();
        }

        public async Task AddPaymentAsync(Payment newPaymnet)
        {
            await _firebaseRepository.AddAsync<Payment>(newPaymnet);
        }

        public async Task AddPaymentsListAsync(List<Payment> newPaymnets)
        {
            foreach (var newPaymnet in newPaymnets)
            {
                await _firebaseRepository.AddAsync<Payment>(newPaymnet);
            }
        }
    }

    public interface IHistoryPaymentService
    {
        Task<List<Payment>> GetPaymentsAsync(string userId);
        Task AddPaymentAsync(Payment newPaymnet);
        Task AddPaymentsListAsync(List<Payment> newPaymnets);
    }
}
