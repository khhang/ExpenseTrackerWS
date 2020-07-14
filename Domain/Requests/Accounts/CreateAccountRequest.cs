using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Accounts
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public int AccountTypeId { get; set; }
    }
}
