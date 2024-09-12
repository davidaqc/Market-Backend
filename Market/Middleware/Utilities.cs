using System.ComponentModel.DataAnnotations;

namespace Market.Middleware
{
    public class Utilities
    {
        internal string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        internal string GenerateValidationCode()
        {
            return Guid.NewGuid().ToString();
        }

        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
