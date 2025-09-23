using UniSystem.Application.Common.Interfaces;

namespace UniSystem.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // In a real application, use a strong hashing algorithm like BCrypt.Net or Argon2
            // For simplicity, we'll just return the password itself for now.
            return password;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // In a real application, verify the hashed password
            return password == hashedPassword;
        }
    }
}