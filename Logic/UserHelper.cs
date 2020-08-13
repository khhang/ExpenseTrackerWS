using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace expense_tracker.Logic
{
    public static class UserHelper
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string GetHashedPassword(string salt, string password)
        {
            var b = Encoding.ASCII.GetBytes(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: b,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
