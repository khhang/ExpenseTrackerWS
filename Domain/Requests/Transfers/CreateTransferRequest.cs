using System;

namespace expense_tracker.Domain.Requests.Transfers
{
    public class CreateTransferRequest
    {
        public int SourceAccountId { get; set; }
        public int? DestinationAccountId { get; set; }
        public int? IncomeCategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
