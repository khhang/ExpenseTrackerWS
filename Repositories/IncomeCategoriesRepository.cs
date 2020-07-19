using expense_tracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface IIncomeCategoriesRepository
    {
        Task<IEnumerable<IncomeCategory>> GetIncomeCategories();
        Task CreateIncomeCategory(string name);
        Task UpdateIncomeCategory(int id, string name);
        Task DeleteIncomeCategory(int id);
    }

    public class IncomeCategoriesRepository : IIncomeCategoriesRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;

        public IncomeCategoriesRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateIncomeCategory(string name)
        {
            var parameters = new
            {
                Name = name
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddIncomeCategory", parameters);
        }

        public Task DeleteIncomeCategory(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteIncomeCategory_ById", parameters);
        }

        public Task<IEnumerable<IncomeCategory>> GetIncomeCategories()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<IncomeCategory>("sp_SelectIncomeCategories");
        }

        public Task UpdateIncomeCategory(int id, string name)
        {
            var parameters = new
            {
                Id = id,
                Name = name
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_UpdateIncomeCategory_ById", parameters);
        }
    }
}
