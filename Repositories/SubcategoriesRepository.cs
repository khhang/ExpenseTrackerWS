using expense_tracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface ISubcategoriesRepository
    {
        Task<IEnumerable<ExpenseSubcategory>> GetExpenseSubcategories();
        Task CreateExpenseSubcategory(string name, int categoryId);
        Task UpdateExpenseSubcategory(int id, string name, int categoryId);
        Task DeleteExpenseSubcategory(int id);
    }

    public class SubcategoriesRepository : ISubcategoriesRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;
        public SubcategoriesRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateExpenseSubcategory(string name, int categoryId)
        {
            var parameters = new
            {
                Name = name,
                CategoryId = categoryId
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddExpenseSubcategory", parameters);
        }

        public Task DeleteExpenseSubcategory(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteExpenseSubcategory_ById", parameters);
        }

        public Task<IEnumerable<ExpenseSubcategory>> GetExpenseSubcategories()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<ExpenseSubcategory>("sp_SelectExpenseSubcategories");
        }

        public Task UpdateExpenseSubcategory(int id, string name, int categoryId)
        {
            var paramaters = new
            {
                Id = id,
                Name = name,
                CategoryId = categoryId
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_UpdateExpenseSubcategory_ById", paramaters);
        }
    }
}
