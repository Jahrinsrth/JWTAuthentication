using JWTAuthentication.DTO;
using JWTAuthentication.Model;

namespace JWTAuthentication.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        public UserAuthService(IPasswordService passwordService, ITokenService tokenService) 
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<User> GetPasswordHash(UserDTO userDTO)
        {
            string salt = _passwordService.GetRandomSalt();
            string passwordHash = _passwordService.GetHashPassword(userDTO.Password, salt);

            User user = new()
            {
                Email = userDTO.Email,
                Password = passwordHash,
            };

            return user;
        }

        public string GetJWTToken(UserDTO userDTO) 
        {
            return _tokenService.GenerateJwtToken(userDTO);
        }
    }
}
