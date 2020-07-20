using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Expenses;
using expense_tracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface IExpensesService
    {
        Task<IEnumerable<ExpenseDetail>> GetExpenseDetails();
        Task UpdateExpense(int id, UpdateExpenseRequest request);
        Task CreateExpense(CreateExpenseRequest request);
        Task DeleteExpense(int id);
    }

    public class ExpensesService : IExpensesService
    {
        public readonly IExpensesRepository _expensesRepository;
        public ExpensesService(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }
        public async Task CreateExpense(CreateExpenseRequest request)
        {
            await _expensesRepository.CreateExpense(
                request.Description, request.Amount, 
                request.ExpenseCategoryId, 
                request.ExpenseSubcategoryId, 
                request.AccountId, 
                request.CreateDate);
        }

        public async Task DeleteExpense(int id)
        {
            await _expensesRepository.DeleteExpense(id);
        }

        public async Task<IEnumerable<ExpenseDetail>> GetExpenseDetails()
        {
            return await _expensesRepository.GetExpenseDetails();
        }

        public async Task UpdateExpense(int id, UpdateExpenseRequest request)
        {
            await _expensesRepository.UpdateExpense(
                id,
                request.Description,
                request.Amount,
                request.ExpenseCategoryId,
                request.ExpenseSubcategoryId,
                request.AccountId,
                request.CreateDate);
        }
    }
}
