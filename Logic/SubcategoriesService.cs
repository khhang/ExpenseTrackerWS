using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Categories;
using expense_tracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface ISubcategoriesService
    {
        Task<IEnumerable<ExpenseSubcategory>> GetExpenseSubcategories();
        Task CreateExpenseSubcategory(CreateSubcategoryRequest request);
        Task UpdateExpenseSubcategory(int id, UpdateSubcategoryRequest request);
        Task DeleteExpenseSubcategory(int id);
    }

    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly ISubcategoriesRepository _subcategoriesRepository;
        public SubcategoriesService(ISubcategoriesRepository subcategoriesRepository)
        {
            _subcategoriesRepository = subcategoriesRepository;
        }

        public async Task CreateExpenseSubcategory(CreateSubcategoryRequest request)
        {
            await _subcategoriesRepository.CreateExpenseSubcategory(request.Name, request.CategoryId);
        }

        public async Task DeleteExpenseSubcategory(int id)
        {
            await _subcategoriesRepository.DeleteExpenseSubcategory(id);
        }

        public async Task<IEnumerable<ExpenseSubcategory>> GetExpenseSubcategories()
        {
            return await _subcategoriesRepository.GetExpenseSubcategories();
        }

        public async Task UpdateExpenseSubcategory(int id, UpdateSubcategoryRequest request)
        {
            await _subcategoriesRepository.UpdateExpenseSubcategory(id, request.Name, request.CategoryId);
        }
    }
}
