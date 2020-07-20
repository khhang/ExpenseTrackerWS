using expense_tracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<ExpenseDetail>> GetExpenseDetails();
        Task CreateExpense(string description, 
            decimal amount, 
            int expenseCategoryId, 
            int expenseSubcategoryId, 
            int accountId, 
            DateTime createDate, 
            DateTime? modifyDate = null);

        Task UpdateExpense(int id,
            string description,
            decimal amount,
            int expenseCategoryId,
            int expenseSubcategoryId,
            int accountId,
            DateTime createDate,
            DateTime? modifyDate = null);

        Task DeleteExpense(int id);
    };

    public class ExpensesRepository : IExpensesRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;
        public ExpensesRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateExpense(string description, decimal amount, int expenseCategoryId, int expenseSubcategoryId, int accountId, DateTime createDate, DateTime? modifyDate = null)
        {
            var parameters = new
            {
                Description = description,
                Amount = amount,
                ExpenseCategoryId = expenseCategoryId,
                ExpenseSubcategoryId = expenseSubcategoryId,
                AccountId = accountId,
                CreateDate = createDate,
                ModifyDate = modifyDate
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddExpense", parameters);
        }

        public Task DeleteExpense(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteExpense_ById", parameters);
        }

        public Task<IEnumerable<ExpenseDetail>> GetExpenseDetails()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<ExpenseDetail>("sp_SelectExpenseDetails");
        }

        public Task UpdateExpense(int id, string description, decimal amount, int expenseCategoryId, int expenseSubcategoryId, int accountId, DateTime createDate, DateTime? modifyDate = null)
        {
            var parameters = new
            {
                Id = id,
                Description = description,
                Amount = amount,
                ExpenseCategoryId = expenseCategoryId,
                ExpenseSubcategoryId = expenseSubcategoryId,
                AccountId = accountId,
                CreateDate = createDate,
                ModifyDate = modifyDate
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_UpdateExpense_ById", parameters);
        }
    }
}
