using System;
using System.Text.RegularExpressions;

namespace UserRegistration
{
    public class PasswordValidator : IPasswordValidator
    {
        
        // Förkompilerad regex som validerar alla krav i en enda sökning
        private static readonly Regex PasswordRegex = new Regex(
            @"^(?=.*[A-ZÅÄÖ])(?=.*[0-9])(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{8,}$",
            RegexOptions.Compiled);
        public bool IsValidPassword(string password)
        {
            // Kontrollera om lösenordet är null, tomt eller bara mellanslag
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Validera lösenordet mot regex-mönstret
            return PasswordRegex.IsMatch(password);
        }
    }
}

// Interface för att möjliggöra mockning i tester
public interface IPasswordValidator
{
    bool IsValidPassword(string password);
}


