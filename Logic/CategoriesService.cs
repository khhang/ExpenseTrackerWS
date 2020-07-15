using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Categories;
using expense_tracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface ICategoriesService
    {
        Task<IEnumerable<ExpenseCategory>> GetExpenseCategories();
        Task UpdateExpenseCategory(int id, UpdateCategoryRequest request);
        Task DeleteExpenseCategory(int id);
        Task CreateExpenseCategory(CreateCategoryRequest request);
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task CreateExpenseCategory(CreateCategoryRequest request)
        {
            await _categoriesRepository.CreateExpenseCategory(request.Name);
        }

        public async Task DeleteExpenseCategory(int id)
        {
            await _categoriesRepository.DeleteExpenseCategory(id);
        }

        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            return await _categoriesRepository.GetExpenseCategories();
        }

        public async Task UpdateExpenseCategory(int id, UpdateCategoryRequest request)
        {
            await _categoriesRepository.UpdateExpenseCategory(id, request.Name);
        }
    }
}
