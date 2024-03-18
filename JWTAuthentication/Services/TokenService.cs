using JWTAuthentication.Common;
using JWTAuthentication.DTO;
using JWTAuthentication.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTAuthentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value;
        }

        public string GenerateJwtToken(UserDTO user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            var sigingInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // properties of JWT token 
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: sigingInCredentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken() 
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            return refreshToken;
        }

    }
}
