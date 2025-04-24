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
        private readonly IMonthRepository _monthRepository;
        public UserService(IUserRepository userRepository,IMonthRepository monthRepository, IAuthService auth)
        {
            _userRepository = userRepository;
            _monthRepository = monthRepository;
            _authService = auth;
            
        }


        public async Task<User?> Register(User user)
        {
            if (user == null) return null;

            Console.WriteLine($"[LOG] לפני הצפנה: {user.Password}");
            user.Password = PasswordHelper.GenerateHashedPassword(user.Password, user);
            Console.WriteLine($"[LOG] אחרי הצפנה: {user.Password}");

            var createdUser = await _userRepository.CreateUserAsync(user);
            if (createdUser == null) return null;

            var newMonth = new Month
            {
                UserId = createdUser.UserId,
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                Expenses = new List<Expense>(),
                Incomes = new List<Income>()
            };

            await _monthRepository.CreateMonthAsync(newMonth);

            return createdUser;
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

        public async Task<User?> Delete(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
      
        public async Task<User?> UpdateUserAsync(int id, string currentPassword, User userUpdate)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            user.Name = userUpdate.Name;
            user.Email = userUpdate.Email;
            user.IsBusiness = userUpdate.IsBusiness;

            bool wantsToChangePassword = !string.IsNullOrEmpty(userUpdate.Password);            
            if (wantsToChangePassword)
            {
                if (string.IsNullOrEmpty(currentPassword)) return null;

                bool isValid = PasswordHelper.VerifyPassword(currentPassword, user.Password, user);
                if (!isValid) return null;

                user.Password = PasswordHelper.GenerateHashedPassword(userUpdate.Password, user);
            }

            return await _userRepository.UpdateUserAsync(id, user);
        }



    }
}
