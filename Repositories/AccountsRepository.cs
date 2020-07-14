using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using expense_tracker.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace expense_tracker.Repositories
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<IEnumerable<AccountDetail>> GetAccountDetails();
        Task<AccountDetail> GetAccountById(int id);
        Task DeleteAccountById(int id);
        Task CreateAccount(string name, decimal startingBalance, int accountTypeId);
        Task<IEnumerable<AccountType>> GetAccountTypes();
    }

    public class AccountsRepository : IAccountsRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;
        public AccountsRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task<IEnumerable<Account>> GetAccounts()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<Account>("sp_SelectAccountDetails");
        }

        public Task<IEnumerable<AccountDetail>> GetAccountDetails()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<AccountDetail>("sp_SelectAccountDetails");
        }

        public Task<AccountDetail> GetAccountById(int id)
        {
            var parameters = new
            {
                AccountId = id
            };

            return _databaseAccessor.QueryProcedureAsync<AccountDetail>("sp_SelectAccountDetails_ById", parameters);
        }

        public Task DeleteAccountById(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteAccount_ById", parameters);
        }

        public Task CreateAccount(string name, decimal startingBalance, int accountTypeId)
        {
            var parameters = new
            {
                Name = name,
                StartingBalance = startingBalance,
                AccountTypeId = accountTypeId
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddAccount", parameters);
        }

        public Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<AccountType>("sp_SelectAccountTypes");
        }
    }
}
