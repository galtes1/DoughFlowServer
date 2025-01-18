using AccountManagementServer.Application.Interface;
using AccountManagementServer.Application.Utils;
using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IAuthService auth)
        {
            _userRepository = userRepository;
            _authService = auth;
        }
        public async Task<User?> Register(User user)
        {
            if (user == null) return null;
            user.Password = PasswordHelper.GenerateHashedPassword(user.Password, user);
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<string?> Login(LoginModel login)
        {
            User? u = await _userRepository.GetUserByEmailAsync(login.Email);

            if (u == null || !PasswordHelper.VerifyPassword(login.Password, u.Password, u))
            {
                return null;
            }

            return _authService.GenerateToken(u);
        }

        public async Task<User?> GetOne(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> Update(int id, User user)
        {
            return await _userRepository.UpdateUserAsync(id, user);
        }

        public async Task<User?> Delete(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
