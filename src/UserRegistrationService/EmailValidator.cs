using System.Text.RegularExpressions;

namespace UserRegistration
{
    public class EmailValidator : IEmailValidator
    {
        // Skapar en statisk och förkompilerad regex för e-postvalidering
        private static readonly Regex EmailRegex = new Regex(
            @"^(?i)[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$",
            RegexOptions.Compiled); // Förkompilerar regex för bättre prestanda
        public bool IsValidEmail(string email)
        {
            // Kontrollera om e-postadressen är null, tom eller endast innehåller mellanslag
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Använder regexen för att matcha e-postadressen och returnerar resultatet
            return EmailRegex.IsMatch(email);
        }
    }

    // Interface för att möjliggöra mockning i tester
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
    }

}
