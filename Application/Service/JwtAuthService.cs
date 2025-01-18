using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountManagementServer.Application.Service
{
    public class JwtAuthService : IAuthService
    {
        public string GenerateToken(User u)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("_id", u.Id.ToString()),
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b0deb9b3-2770-49f1-9c7f-accdce966e76"));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "AccountManagementServer",
                audience: "AccountManagementReactApp",
                expires: DateTime.Now.AddDays(2),
                claims: claims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
