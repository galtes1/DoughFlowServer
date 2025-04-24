using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> CreateUserAsync(User user);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> UpdateUserAsync(int id, User user);
        public Task<User?> DeleteUserAsync(int id);
        public Task<bool> IsCurrentPasswordValidAsync(int userId, string currentPassword);

    }
}