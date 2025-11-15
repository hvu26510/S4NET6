using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace S4NET6.Services
{
    public interface IJwtServices
    {
        string GenToken(string username, string role);
    }

    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration _configuration;
        public JwtServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenToken(string username, string role)
        {
            var jwtSection = _configuration.GetSection("Jwt");

            //Tao khoa
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));

            //tao thong tin de sign
            var credInfos = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Claim
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            //Tao doi jwt

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSection["ExpireMinutes"])),
                signingCredentials: credInfos
                );
            // tra ve chuoi token
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
