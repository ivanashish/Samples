using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories.DbModels
{
    public enum AccountType { Savings, Current }

    public class Account : BaseModel
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        [Required]
        public AccountType Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal MinimumBalance { get; set; }
    }
}
