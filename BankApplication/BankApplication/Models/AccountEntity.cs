using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public enum AccountType { Savings, Current }

    public class AccountEntity : BaseEntity
    {
        public int CustomerId { get; set; }

        public CustomerEntity Customer { get; set; }

        public int BranchId { get; set; }

        public BranchEntity Branch { get; set; }

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
