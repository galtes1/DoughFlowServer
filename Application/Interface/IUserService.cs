using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Interface
{
    public interface IUserService
    {
        Task<User?> Register(User user);
        Task<string?> Login(LoginModel loginModel);
        Task<User?> GetOne(int id);
        Task<User?> Update(int id, User user);
        Task<User?> Delete(int id);
    }
}
