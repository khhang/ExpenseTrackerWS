using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Categories;
using expense_tracker.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface IIncomeCategoriesService
    {
        Task<IEnumerable<IncomeCategory>> GetIncomeCategories();
        Task CreateIncomeCategory(CreateIncomeCategoryRequest request);
        Task UpdateIncomeCategory(int id, UpdateIncomeCategoryRequest request);
        Task DeleteIncomeCategory(int id);
    }
    public class IncomeCategoriesService : IIncomeCategoriesService
    {
        private readonly IIncomeCategoriesRepository _incomeCategoriesRepository;
        public IncomeCategoriesService(IIncomeCategoriesRepository incomeCategoriesRepository)
        {
            _incomeCategoriesRepository = incomeCategoriesRepository;
        }

        public async Task CreateIncomeCategory(CreateIncomeCategoryRequest request)
        {
            await _incomeCategoriesRepository.CreateIncomeCategory(request.Name);
        }

        public async Task DeleteIncomeCategory(int id)
        {
            await _incomeCategoriesRepository.DeleteIncomeCategory(id);
        }

        public async Task<IEnumerable<IncomeCategory>> GetIncomeCategories()
        {
            return await _incomeCategoriesRepository.GetIncomeCategories();
        }

        public async Task UpdateIncomeCategory(int id, UpdateIncomeCategoryRequest request)
        {
            await _incomeCategoriesRepository.UpdateIncomeCategory(id, request.Name);
        }
    }
}
