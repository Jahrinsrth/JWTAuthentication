using JWTAuthentication.DTO;
using JWTAuthentication.Model;
using System.Security.Claims;

namespace JWTAuthentication.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAuthService(IPasswordService passwordService, 
            ITokenService tokenService, IHttpContextAccessor httpContextAccessor) 
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
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

        public string GetUserDetails()
        {
            var result = string.Empty;

            // reading claim using IHttpContextAccessor
            if (_httpContextAccessor is not null) 
            {
                //result = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                //result = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
                result = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
    }
}
