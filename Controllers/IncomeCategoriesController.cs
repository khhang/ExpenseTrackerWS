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
    public class IncomeCategoriesController : ControllerBase
    {
        private readonly IIncomeCategoriesService _incomeCategoriesService;
        public IncomeCategoriesController(IIncomeCategoriesService incomeCategoriesService)
        {
            _incomeCategoriesService = incomeCategoriesService;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<IncomeCategory>> GetIncomeCategories()
        {
            return await _incomeCategoriesService.GetIncomeCategories();
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateIncomeCategory([FromBody]CreateIncomeCategoryRequest request)
        {
            await _incomeCategoriesService.CreateIncomeCategory(request);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateIncomeCategory(int id, [FromBody]UpdateIncomeCategoryRequest request)
        {
            await _incomeCategoriesService.UpdateIncomeCategory(id, request);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteIncomeCategory(int id)
        {
            await _incomeCategoriesService.DeleteIncomeCategory(id);
            return Ok();
        }
    }
}
