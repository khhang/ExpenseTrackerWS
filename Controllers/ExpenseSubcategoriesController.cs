using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Categories;
using expense_tracker.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseSubcategoriesController : ControllerBase
    {
        private readonly ISubcategoriesService _subcategoriesService;
        public ExpenseSubcategoriesController(ISubcategoriesService subcategoriesService)
        {
            _subcategoriesService = subcategoriesService;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ExpenseSubcategory>> GetExpenseSubcategories()
        {
            return await _subcategoriesService.GetExpenseSubcategories();
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateExpenseSubcategory([FromBody]CreateSubcategoryRequest request)
        {
            await _subcategoriesService.CreateExpenseSubcategory(request);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateExpenseSubcategory(int id, [FromBody]UpdateSubcategoryRequest request)
        {
            await _subcategoriesService.UpdateExpenseSubcategory(id, request);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteExpenseSubcategory(int id)
        {
            await _subcategoriesService.DeleteExpenseSubcategory(id);
            return Ok();
        }
    }
}
