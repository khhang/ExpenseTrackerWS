using expense_tracker.Controllers;
using expense_tracker.Infrastructure;
using expense_tracker.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface IUsersService
    {
        Task CreateUser(UserCredential userCredentials);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private const int PASSWORD_MIN_LENGTH = 8;
        private const int PASSWORD_MAX_LENGTH = 50;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateUser(UserCredential userCredentials)
        {
            // no duplicate usernames
            var users = await _usersRepository.SearchUser(userCredentials.Username.Trim());

            if (users.Any())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"User with username: {userCredentials.Username} already exists");
            }

            if(userCredentials.Password.Length < PASSWORD_MIN_LENGTH)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"Password does not meet the minimum of {PASSWORD_MIN_LENGTH} characters");
            }

            if (userCredentials.Password.Length > PASSWORD_MAX_LENGTH)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"Password exceeds the maximum limit of {PASSWORD_MAX_LENGTH} characters");
            }

            var salt = UserHelper.GenerateSalt();
            var hashedPassword = UserHelper.GetHashedPassword(userCredentials.Password + salt, userCredentials.Password);

            await _usersRepository.CreateUser(userCredentials.Username, salt, hashedPassword);
        }
    }
}
