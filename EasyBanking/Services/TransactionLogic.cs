using EasyBanking.DataAccess.Model.Repository;
using EasyBanking.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyBanking.Services
{
    public class TransactionLogic : ITransactionLogic
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionCurrencyConverterService _transactionCurrencyConverterService;

        public TransactionLogic(ITransactionRepository transactionRepository, ITransactionCurrencyConverterService transactionCurrencyConverterService)
        {
            _transactionRepository = transactionRepository;
            _transactionCurrencyConverterService = transactionCurrencyConverterService;
        }

        public async Task<bool> SetAccountBalanceForCreditAndDebit(TransactionDto transaction)
        {
            var latestTransaction = await _transactionRepository.GetLatestTransactionAsync(transaction.AccountId);
            //When Amount is Credited
            if (transaction.TransactionTypeId == 1)
            {
                transaction.AccountBalance = latestTransaction.AccountBalance + transaction.TransactionAmount;
            }
            //When Amount is Debited
            else if (transaction.TransactionTypeId == 2)
            {
                if (latestTransaction.AccountBalance < transaction.TransactionAmount)
                {
                    return false;
                }
                transaction.AccountBalance = latestTransaction.AccountBalance - transaction.TransactionAmount;
            }
            return true;
        }

        public async Task<TransactionDto> SetTransactionAmountToBaseCurrency(TransactionDto transactionDto)
        {
            if(!string.IsNullOrEmpty(transactionDto.TransactionCurrency))
            {
                var rateObj = await _transactionCurrencyConverterService.GetAllConversionValues(transactionDto.TransactionCurrency);
                transactionDto.TransactionAmount = transactionDto.TransactionAmount * Convert.ToDecimal(rateObj.rates.GBP);
            }
            return transactionDto;

        }
    }
}
