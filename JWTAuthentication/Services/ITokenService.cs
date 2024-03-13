using JWTAuthentication.DTO;

namespace JWTAuthentication.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDTO user);
    }
}

