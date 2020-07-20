using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Models
{
    public class ExpenseDetail : Expense
    {
        public string AccountName { get; set; }
        public string ExpenseCategoryName { get; set; }
        public string ExpenseSubcategoryName { get; set; }
    }
}
