using EasyBanking.DataAccess.Model;
using EasyBanking.DataAccess.Model.Repository;
using EasyBanking.Dtos;
using EasyBanking.Model;
using EasyBanking.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace EasyBanking.Tests.ServicesTests
{
    [TestClass]
    public class TransactionLogicTests
    {

        [TestMethod]
        public async Task WhenAmountCreditedSetAccountBalanceMoreThanPreviousBalance()
        {
            //Arrange
            TransactionDto transactionDto = new()
            {
                TransactionAmount = 1000,
                TransactionTypeId = 1,
                AccountId = 1
            };
            Transaction transaction = new()
            {
                TransactionId = 5,
                TransactionAmount = 2000,
                AccountBalance = 3000,
                AccountId = 1
            };
            Mock<ITransactionRepository> mockTransactionRepository = new();
            mockTransactionRepository.Setup(f => f.GetLatestTransactionAsync(transactionDto.AccountId)).Returns(Task.FromResult(transaction));
            Mock<ITransactionCurrencyConverterService> mockTransactionCurrencyConverterService = new();
            TransactionLogic transactionLogic = new TransactionLogic(mockTransactionRepository.Object, mockTransactionCurrencyConverterService.Object);

            //Act
            var result = await transactionLogic.SetAccountBalanceForCreditAndDebit(transactionDto);

            //Assert
            Assert.AreEqual(4000, transactionDto.AccountBalance);
            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public async Task WhenAmountDebitedSetAccountBalanceLessThanPreviousBalance()
        {
            //Arrange
            TransactionDto transactionDto = new()
            {
                TransactionAmount = 1000,
                TransactionTypeId = 2,
                AccountId = 1
            };
            Transaction transaction = new()
            {
                TransactionId = 5,
                TransactionAmount = 2000,
                AccountBalance = 3000,
                AccountId = 1
            };
            Mock<ITransactionRepository> mockTransactionRepository = new();
            mockTransactionRepository.Setup(f => f.GetLatestTransactionAsync(transactionDto.AccountId)).Returns(Task.FromResult(transaction));
            Mock<ITransactionCurrencyConverterService> mockTransactionCurrencyConverterService = new();
            TransactionLogic transactionLogic = new(mockTransactionRepository.Object, mockTransactionCurrencyConverterService.Object);

            //Act
            var result = await transactionLogic.SetAccountBalanceForCreditAndDebit(transactionDto);

            //Assert
            Assert.AreEqual(2000, transactionDto.AccountBalance);
            Assert.AreEqual(result, true);

        }


        [TestMethod]
        public async Task WhenAmountDebitedIsMoreThanAvailableBalanceReturnsFalse()
        {
            //Arrange
            TransactionDto transactionDto = new()
            {
                TransactionAmount = 4000,
                TransactionTypeId = 2,
                AccountId = 1
            };
            Transaction transaction = new()
            {
                TransactionId = 5,
                TransactionAmount = 2000,
                AccountBalance = 3000,
                AccountId = 1
            };
            Mock<ITransactionRepository> mockTransactionRepository = new();
            mockTransactionRepository.Setup(f => f.GetLatestTransactionAsync(transactionDto.AccountId)).Returns(Task.FromResult(transaction));
            Mock<ITransactionCurrencyConverterService> mockTransactionCurrencyConverterService = new();
            TransactionLogic transactionLogic = new(mockTransactionRepository.Object, mockTransactionCurrencyConverterService.Object);

            //Act
            var result = await transactionLogic.SetAccountBalanceForCreditAndDebit(transactionDto);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public async Task WhenTransactionAmountInDiffCurrencyConvertAndSetTransactionAmountinGBP()
        {
            //Arrange
            TransactionDto transactionDto = new()
            {
                TransactionAmount = 1000,
                TransactionTypeId = 1,
                TransactionCurrency="EUR",
                AccountId = 1
            };
            Transaction transaction = new()
            {
                TransactionId = 5,
                TransactionAmount = 2000,
                AccountBalance = 3000,
                AccountId = 1
            };
            Rates rates = new()
            {
                GBP = 0.87
            };

            RateObj rateObj = new()
            {
                rates = rates
            };
            Mock<ITransactionRepository> mockTransactionRepository = new();
            Mock<ITransactionCurrencyConverterService> mockTransactionCurrencyConverterService = new();
            mockTransactionCurrencyConverterService.Setup(t => t.GetAllConversionValues(It.IsAny<string>())).Returns(Task.FromResult(rateObj));
            TransactionLogic transactionLogic = new(mockTransactionRepository.Object, mockTransactionCurrencyConverterService.Object);

            //Act
            var result = await transactionLogic.SetTransactionAmountToBaseCurrency(transactionDto);

            //Assert
            Assert.AreEqual(870.00M, transactionDto.TransactionAmount);

        }

    }
}
