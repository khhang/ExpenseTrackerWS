using System.Collections.Generic;
using System.Threading.Tasks;
using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Accounts;
using expense_tracker.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Logic
{
    public interface IAccountsService
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<IEnumerable<AccountDetail>> GetAccountDetails();
        Task<AccountDetail> GetAccountById(int id);
        Task DeleteAccountById(int id);
        Task CreateAccount(CreateAccountRequest request);
        Task<IEnumerable<AccountType>> GetAccountTypes();
        Task UpdateAccountById(int id, UpdateAccountRequest request);
    }

    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;
        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountsRepository.GetAccounts();
        }

        public async Task<IEnumerable<AccountDetail>> GetAccountDetails()
        {
            return await _accountsRepository.GetAccountDetails();
        }

        public async Task<AccountDetail> GetAccountById(int id)
        {
            return await _accountsRepository.GetAccountById(id);
        }

        public async Task DeleteAccountById(int id)
        {
            await _accountsRepository.DeleteAccountById(id);
        }

        public async Task CreateAccount(CreateAccountRequest request)
        {
            await _accountsRepository.CreateAccount(request.Name, request.StartingBalance, request.AccountTypeId);
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            return await _accountsRepository.GetAccountTypes();
        }

        public async Task UpdateAccountById(int id, UpdateAccountRequest request)
        {
            await _accountsRepository.UpdateAccountById(id, request.Name, request.StartingBalance, request.AccountTypeId);
        }
    }
}
