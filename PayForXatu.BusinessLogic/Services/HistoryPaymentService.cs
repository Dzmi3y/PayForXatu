using PayForXatu.Database.Models;
using PayForXatu.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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

        public async Task<Dictionary<DateTime,List<Payment>>> GetPaymentsByNameAndPeriodListAsync(string userId,
            DateTime startDate, DateTime endDate, List<string> paymentsDate)
        {
            paymentsDate ??= new List<string>();

            var allPayments = await _firebaseRepository.GetListOfChildsAsync<Payment>();
            var result = allPayments.Where(x =>
                             (x.UserId == userId) &&
                             (paymentsDate.Contains(x.PaymentName) || paymentsDate.Count == 0) &&
                             ((startDate <= x.Date) && (x.Date <= endDate))
                          ).GroupBy(x => x.Date.Date)
                          .ToDictionary(g=>g.Key,g=>g.ToList());

            return result ?? new Dictionary<DateTime, List<Payment>>();
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

        public async Task<List<string>> GetPaymentNamesAsync(string userId)
        {
            var allPayments = await _firebaseRepository.GetListOfChildsAsync<Payment>();
            var result = allPayments.Where(x => x.UserId == userId)
                .Select(x => x.PaymentName)
                .Distinct()
                .ToList();

            return result ?? new List<string>();
        }

    }

    public interface IHistoryPaymentService
    {
        Task<List<Payment>> GetPaymentsAsync(string userId);
        Task AddPaymentAsync(Payment newPaymnet);
        Task AddPaymentsListAsync(List<Payment> newPaymnets);
        Task<Dictionary<DateTime, List<Payment>>> GetPaymentsByNameAndPeriodListAsync(string userId,
            DateTime startDate, DateTime endDate, List<string> paymentsDate);
        Task<List<string>> GetPaymentNamesAsync(string userId);
    }
}
