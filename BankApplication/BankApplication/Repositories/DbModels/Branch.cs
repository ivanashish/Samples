﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories.DbModels
{
    public class Branch : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string IFSC { get; set; }

        [Required]
        [StringLength(2000)]
        public string Address { get; set; }
    }
}
