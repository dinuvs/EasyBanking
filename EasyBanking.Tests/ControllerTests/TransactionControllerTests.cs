using AutoMapper;
using EasyBanking.Controllers;
using EasyBanking.DataAccess.Model;
using EasyBanking.DataAccess.Model.Repository;
using EasyBanking.Dtos;
using EasyBanking.Services;
using EasyBanking.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;


namespace EasyBanking.Tests.ControllerTests
{
    [TestClass]
    public class TransactionControllerTests
    {
        [TestMethod]
        public void GetListOfTransactionsReturnsSuccessStatus()
        {

            //Arrange
            Transaction transaction1 = new()
            {
                TransactionId = 1,
                TransactionAmount = 1000,
                AccountBalance = 1000,
                TransactionTypeId = 1
            };
            Transaction transaction2 = new()
            {
                TransactionId = 2,
                TransactionAmount = 500,
                AccountBalance = 1500,
                TransactionTypeId = 1
            };
            List<Transaction> transactions = new();
            transactions.Add(transaction1);
            transactions.Add(transaction2);

            Mock<IMapper> mockMapper = new();
            mockMapper.Setup(m => m.Map<IEnumerable<TransactionDto>>(It.IsAny<IEnumerable<Transaction>>()));
            Mock<ITransactionRepository> mockTransactionRepository = new();
            mockTransactionRepository.Setup(f => f.GetAllTransactions(1)).Returns(transactions.AsEnumerable());
            Mock<IObjectJsonConverter> mockObjJsonConverter = new();
            Mock<IAccountDetails> mockAccountDetails = new();
            mockAccountDetails.Setup(a => a.AccountId).Returns(1);
            Mock<ITransactionLogic> mockTransactionLogic = new();

            TransactionController transactionController = new(mockTransactionRepository.Object, mockMapper.Object,
               mockObjJsonConverter.Object, new NullLogger<TransactionController>(),
               mockAccountDetails.Object, mockTransactionLogic.Object);

            //Act
            var result= (OkObjectResult)transactionController.Index();

            //Assert
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
