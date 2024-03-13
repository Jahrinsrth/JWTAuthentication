using JWTAuthentication.DTO;
using JWTAuthentication.Model;

namespace JWTAuthentication.Services
{
    public interface IUserAuthService
    {
        Task<User> GetPasswordHash(UserDTO userDTO);
        string GetJWTToken(UserDTO userDTO);
    }
}
