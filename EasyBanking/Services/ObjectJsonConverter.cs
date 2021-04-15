using EasyBanking.DataAccess.Model;
using EasyBanking.Dtos;
using Newtonsoft.Json;

namespace EasyBanking.Services
{
    public class ObjectJsonConverter : IObjectJsonConverter
    {
        public TransactionDto ConvertTransactionJsonDataToObj(string jsonData)
        {
            return JsonConvert.DeserializeObject<TransactionDto>(jsonData);
        }

    }
}
