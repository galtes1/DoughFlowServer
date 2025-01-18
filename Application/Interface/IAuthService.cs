using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Interface
{
    public interface IAuthService
    {
        public string GenerateToken(User u);
    }
}
