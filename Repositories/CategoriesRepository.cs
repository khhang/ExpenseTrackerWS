using expense_tracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<ExpenseCategory>> GetExpenseCategories();
        Task CreateExpenseCategory(string name);
        Task UpdateExpenseCategory(int id, string name);
        Task DeleteExpenseCategory(int id);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;
        public CategoriesRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateExpenseCategory(string name)
        {
            var parameters = new
            {
                Name = name
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddExpenseCategory", parameters);
        }

        public Task DeleteExpenseCategory(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteExpenseCategory_ById", parameters);
        }

        public Task<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<ExpenseCategory>("sp_SelectExpenseCategories");
        }

        public Task UpdateExpenseCategory(int id, string name)
        {
            var parameters = new
            {
                Id = id,
                Name = name
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_UpdateExpenseCategory_ById", parameters);
        }
    }
}
