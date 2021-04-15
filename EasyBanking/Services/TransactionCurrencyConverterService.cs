using EasyBanking.Model;
using EasyBanking.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EasyBanking.Services
{
    public class TransactionCurrencyConverterService: ITransactionCurrencyConverterService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient _restClient;

        public TransactionCurrencyConverterService(IConfiguration configuration, IRestClient restClient)
        {
            _configuration = configuration;
            _restClient = restClient;
        }

        public async Task<RateObj> GetAllConversionValues(string conversionCurrency)
        {
            var test=_configuration.GetSection("RatesApiIo");
            
            var jsonData=await _restClient.GetAsync(string.Concat(test.Value, conversionCurrency));
            if(jsonData != null)
            {
                var ratesObj = JsonConvert.DeserializeObject<RateObj>(jsonData);
                return ratesObj;
            }
            return null;
        }
    }
}
