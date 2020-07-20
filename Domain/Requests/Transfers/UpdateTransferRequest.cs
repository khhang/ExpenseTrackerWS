using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Transfers
{
    public class UpdateTransferRequest
    {
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public int IncomeCategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
