using System.Text.RegularExpressions;

namespace UserRegistration
{
    public class UsernameValidator : IUsernameValidator
    {
        // Förkompilerad regex för bättre prestanda
        private static readonly Regex UsernameRegex = new Regex(
            @"^[a-zA-Z0-9]{5,20}$", 
            RegexOptions.Compiled);
        public bool IsValidName(string username)
        {
            // Kontrollera om strängen är tom eller null
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // Validera mot regex-mönstret
            return UsernameRegex.IsMatch(username);
        }
    }
    
    // Interface för att möjliggöra mockning i tester
    public interface IUsernameValidator
    {
        bool IsValidName(string username);
    }
}

