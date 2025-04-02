using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UserRegistrationService.Models;
using UserRegistration;

namespace UserRegistration.Services
{
    public class UserService
    {
        private readonly ConcurrentBag<User> _users = new();

        // Validerare injiceras via konstruktorn för att möjliggöra testbarhet
        private readonly IUsernameValidator _usernameValidator;
        private readonly IEmailValidator _emailValidator;
        private readonly IPasswordValidator _passwordValidator;

        public UserService(
            IUsernameValidator usernameValidator,
            IEmailValidator emailValidator,
            IPasswordValidator passwordValidator)
        {
            _usernameValidator = usernameValidator;
            _emailValidator = emailValidator;
            _passwordValidator = passwordValidator;
        }

        public RegistrationResult RegisterUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Användaren får inte vara null.");
            }
            // Validera användarnamn
            if (string.IsNullOrWhiteSpace(user.Username) || !_usernameValidator.IsValidName(user.Username))
            {
                return new RegistrationResult(false, user.Username, "Användarnamn måste vara 5-20 alfanumeriska tecken");
            }

            // Kontrollera om användarnamnet redan finns
            if (_users.Any(u => u.Username == user.Username))
            {
                return new RegistrationResult(false, user.Username, "Användarnamnet är redan upptaget");
            }

            // Validera lösenord
            if (string.IsNullOrWhiteSpace(user.Password) || !_passwordValidator.IsValidPassword(user.Password))
            {
                return new RegistrationResult(false, user.Username, "Lösenordet måste vara minst 8 tecken och innehålla minst ett specialtecken, en stor bokstav och en siffra");
            }
            
            // Validera e-post
            if (string.IsNullOrWhiteSpace(user.Email) || !_emailValidator.IsValidEmail(user.Email))
            {
                return new RegistrationResult(false, user.Username, "Ogiltig e-postadress");
            }

            // Alla valideringar godkända - registrera användaren
            _users.Add(user);
            return new RegistrationResult(true, user.Username, "Användaren har registrerats");
        }
        
        /// <summary>
        /// Hämtar alla registrerade användare (för teständamål)
        /// </summary>
        /// <returns>Lista av registrerade användare</returns>
        public IEnumerable<User> GetAllUsers() => _users.ToList();
    }
}
