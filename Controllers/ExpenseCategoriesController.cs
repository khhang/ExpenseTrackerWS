using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Categories;
using expense_tracker.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public ExpenseCategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            return await _categoriesService.GetExpenseCategories();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateExpenseCategory([FromBody] CreateCategoryRequest request)
        {
            await _categoriesService.CreateExpenseCategory(request);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateExpenseCategory(int id, [FromBody] UpdateCategoryRequest request)
        {
            await _categoriesService.UpdateExpenseCategory(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteExpenseCategory(int id)
        {
            await _categoriesService.DeleteExpenseCategory(id);
            return Ok();
        }
    }
}
