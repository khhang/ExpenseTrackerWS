using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Models
{
    public class TransferDetail
    {
        public int? TransferLinkId { get; set; }
        public decimal Amount { get; set; }
        public int SourceAccountId { get; set; }
        public string SourceAccountName { get; set; }
        public int DestinationAccountId { get; set; }
        public string DestinationAccountName { get; set; }
        public DateTime CreateDate { get; set; }
        private DateTime? _modifyDate;
        public DateTime ModifyDate
        {
            get { return this._modifyDate ?? DateTime.UtcNow; }
            set { this._modifyDate = value; }
        }
    }
}
