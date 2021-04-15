using AutoMapper;
using EasyBanking.DataAccess.Model;
using EasyBanking.DataAccess.Model.Repository;
using EasyBanking.Dtos;
using EasyBanking.Services;
using EasyBanking.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EasyBanking.Controllers
{
    [ApiController]
    [Route("api/Transaction")]

    public class TransactionController:ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IObjectJsonConverter _objectJsonConverter;
        private readonly ILogger<TransactionController> _logger;
        private readonly IAccountDetails _accountDetails;
        private readonly ITransactionLogic _transactionLogic;

        public TransactionController(ITransactionRepository transactionRepository, IMapper mapper,
               IObjectJsonConverter objectJsonConverter, ILogger<TransactionController> logger, 
               IAccountDetails accountDetails, ITransactionLogic transactionLogic)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _objectJsonConverter = objectJsonConverter;
            _logger = logger;
            _accountDetails = accountDetails;
            _transactionLogic = transactionLogic;
            //Setting it to default account Id 1, needs to be changed later to dynamically select accountId based on login
            _accountDetails.AccountId = 1;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var transactions = _transactionRepository.GetAllTransactions(_accountDetails.AccountId);
            if(transactions != null)
            {
                _logger.LogInformation($"Success:Transactions fetched successfully for Account {_accountDetails.AccountId}");
                return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactions));
            }
            _logger.LogInformation($"Info:No Transactions returned for Account {_accountDetails.AccountId}");
            return NotFound();
        }

        //API to get the latest Account balance and other transaction details
        [HttpGet]
        [Route("LatestTransaction")]
        public async Task<IActionResult> GetLatestTransaction()
        {
            var transaction = await _transactionRepository.GetLatestTransactionAsync(_accountDetails.AccountId);
            if (transaction != null)
            {
                _logger.LogInformation($"Success:Latest Transaction fetched successfully for Account {_accountDetails.AccountId}");
                return Ok(_mapper.Map<TransactionDto>(transaction));
            }
            _logger.LogInformation($"Info:No Transaction returned for Account {_accountDetails.AccountId}");
            return NotFound();
        }

        /// <summary>
        /// Add a Debit or Credit Transaction
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///    { "TransactionAmount": 1000.0, "TransactionCurrency":"EUR",  "AccountId": 1,   "TransactedBy": null,   "LastUpdatedBy": null, TransactionTypeId:1 }
        /// </remarks>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(string request)
        {
            var transactionDto = _objectJsonConverter.ConvertTransactionJsonDataToObj(request);
            if (transactionDto != null)
            {
                if (transactionDto.TransactionAmount < 0)
                {
                     _logger.LogInformation($"Error:Transaction with value {transactionDto.TransactionAmount} for account id {transactionDto.AccountId}  is less than zero, which is not allowed");
                    return Content("Negative amounts cannot be transacted");
                }
                if (!string.IsNullOrEmpty(transactionDto.TransactionCurrency) && transactionDto.TransactionCurrency != "GBP")
                {
                    transactionDto = await _transactionLogic.SetTransactionAmountToBaseCurrency(transactionDto);
                }
                var result = await _transactionLogic.SetAccountBalanceForCreditAndDebit(transactionDto);
                if (!result)
                {
                    _logger.LogInformation($"Error:Transaction with value {transactionDto.TransactionAmount} for account id {transactionDto.AccountId}  is greater than Available Balance");
                    return Content("Transacted Amount is more than the available balance");
                }
                transactionDto.LastUpdated = DateTime.Now;

                var transactionAdded = await _transactionRepository.AddAsync(_mapper.Map<Transaction>(transactionDto));
                if (transactionAdded != null)
                {
                    _logger.LogInformation($"Info:Transaction with value {transactionDto.TransactionAmount} for account id {transactionDto.AccountId}  created successfully");
                    return Ok(_mapper.Map<TransactionDto>(transactionAdded));
                }

                return NotFound();
            }
            return Content("Transaction is empty");
        }

    }
}
