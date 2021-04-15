using EasyBanking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBanking.Services
{
    public interface ITransactionCurrencyConverterService
    {
        Task<RateObj> GetAllConversionValues(string conversionCurrency);
    }
}
