using BCryptNet = BCrypt.Net.BCrypt;

namespace JWTAuthentication.Services
{
    public class PasswordService : IPasswordService
    {
        public string GetRandomSalt()
        {
            return BCryptNet.GenerateSalt();
        }

        public string GetHashPassword(string password, string salt)
        {
            return BCryptNet.HashPassword(password, salt);
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            throw new NotImplementedException();
        }
    }
}
