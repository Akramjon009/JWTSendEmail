using EmailSenderApp.Domain.Entites.Models.AuthModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EmailSenderApp.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(User user)
        {
            if (user == null)
            {
                return "User Will be Null";
            }

            if (UserExist(user))
            {
                var permissions = new List<int>();

                if (user.Role == "Teacher")
                {
                    permissions = new List<int>() { 1, 2, 3, 4 };
                }
                else if (user.Role == "Student")
                {
                    permissions = new List<int>() { 5, 7, 3, 4 };
                }
                else if (user.Role == "Admin")
                {
                    permissions = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                }

                var jsonContent = JsonSerializer.Serialize(permissions);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserName", user.UserName),
                    new Claim("UserID", user.Id.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent),
                };

                return await GenerateToken(claims);
            }
            else
            {
                return "User UnAuthorithe 401";
            }

        }


        public async Task<string> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(_configuration["JWT:ExpireDate"] ?? "10");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exDate),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public bool UserExist(User user)
        {
            var login = "admin";
            var password = "123";

            if (user.Login == login && user.Password == password)
            {
                return true;
            }

            return false;
        }
    }
}
