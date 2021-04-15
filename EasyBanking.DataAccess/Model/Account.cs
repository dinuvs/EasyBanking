using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBanking.DataAccess.Model
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public long AccountNumber { get; set; }
        public string SortCode { get; set; }
        public string AccountName { get; set; }
        public string AccountAddress { get; set; }
        public int AccountCountryCode { get; set; }
        public long AccountPhone { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
