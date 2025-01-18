using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace AccountManagementServer.Application.Utils
{
    public class PasswordHelper
    {
        private static readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public static string GenerateHashedPassword(string password, User user) //register
        {

            user.Password = "";
            return passwordHasher.HashPassword(user, password);
        }

        public static bool VerifyPassword(string password, string hashedPassword, User user) //Login
        {
            user.Password = "";
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, hashedPassword, password);

            if (result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return true;
        }
    }
}
