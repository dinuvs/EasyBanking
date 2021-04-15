using EasyBanking.Dtos;

namespace EasyBanking.Services
{
    public interface IObjectJsonConverter
    {
        TransactionDto ConvertTransactionJsonDataToObj(string jsonData);
    }
}
