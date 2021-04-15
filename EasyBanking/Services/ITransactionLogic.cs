using EasyBanking.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBanking.Services
{
    public interface ITransactionLogic
    {
        Task<bool> SetAccountBalanceForCreditAndDebit(TransactionDto transaction);
        Task<TransactionDto> SetTransactionAmountToBaseCurrency(TransactionDto transactionDto);
    }
}
