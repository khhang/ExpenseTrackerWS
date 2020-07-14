using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Models
{
    public class AccountDetail : Account
    {
        public string AccountTypeName { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
