using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Interface
{
    public interface IUserService
    {
        public Task<User?> Register(User user);
        public Task<string?> Login(LoginModel loginModel);
        public Task<User?> GetOne(int id);
        public Task<User?> Delete(int id);
        public Task<User?> UpdateUserAsync(int id, string currentPassword, User userUpdate);
       
     
    }
}
