using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBanking.DataAccess.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransactionAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AccountBalance { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }
        public string TransactedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }

    }
}
