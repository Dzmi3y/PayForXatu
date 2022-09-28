using PayForXatu.Database;
using PayForXatu.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForXatu.BusinessLogic.Services
{
    public class CurrencyService: ICurrencyService
    {
        private IFirebaseRepository _firebaseRepository;
        public CurrencyService(IFirebaseRepository firebaseRepository)
        {
            _firebaseRepository = firebaseRepository;
        }

        public async Task<List<Currency>> GetCurrencyListAsync()
        {

            return await _firebaseRepository.GetListOfChildsAsync<Currency>();

        }
    }

    public interface ICurrencyService
    {
        Task<List<Currency>> GetCurrencyListAsync();
    }
}
