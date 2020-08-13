using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface IUsersRepository
    {
        Task CreateUser(string username, string salt, string passwordHash);
        Task<IEnumerable<User>> SearchUser(string username);
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;

        public UsersRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateUser(string username, string salt, string passwordHash)
        {
            var parameters = new
            {
                Username = username,
                Salt = salt,
                PasswordHash = passwordHash
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddNewUser", parameters);
        }

        public Task<IEnumerable<User>> SearchUser(string username)
        {
            var parameters = new 
            {
                Username = username
            };

            return _databaseAccessor.QueryMultipleProcedureAsync<User>("sp_SearchUserByUserName", parameters);
        }
    }
}
