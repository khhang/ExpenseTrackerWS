using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Expenses
{
    public class CreateExpenseRequest
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        public int ExpenseSubcategoryId { get; set; }
        public int AccountId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
