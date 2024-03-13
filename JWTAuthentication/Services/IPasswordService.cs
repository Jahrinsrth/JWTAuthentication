namespace JWTAuthentication.Services
{
    public interface IPasswordService
    {
        string GetRandomSalt();
        string GetHashPassword(string password, string salt);
        bool ValidatePassword(string password, string correctHash);
        //Task<bool> IsPwnedPassword(string password);
    }
}
