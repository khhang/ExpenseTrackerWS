using System.Collections.Generic;
using System.Threading.Tasks;
using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Accounts;
using expense_tracker.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountsService.GetAccounts();
        }

        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<AccountDetail>> GetAccountsDetails()
        {
            return await _accountsService.GetAccountDetails();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<AccountDetail> GetAccountById(int id)
        {
            return await _accountsService.GetAccountById(id);
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            return await _accountsService.GetAccountTypes();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountRequest request)
        {
            await _accountsService.CreateAccount(request);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountsService.DeleteAccountById(id);
            return Ok();
        }
    }
}
