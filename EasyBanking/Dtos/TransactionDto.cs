using System;

namespace EasyBanking.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal AccountBalance { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }
        public string TransactionCurrency { get; set; }
        public string TransactedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Error { get; set; }
    }
}
