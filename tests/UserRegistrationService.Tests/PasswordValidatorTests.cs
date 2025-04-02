using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration;

namespace UserRegistrationService.Tests
{
    [TestClass]
    public class PasswordValidatorTests
    {
        private PasswordValidator _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            // Skapa en instans av PasswordValidator
            _validator = new PasswordValidator();
        }

        [TestMethod]
        [DataRow("short", false)] // För kort
        [DataRow("långtlösenordutanpecialtecken", false)] // Inget specialtecken
        [DataRow("Lösenord1!", true)] // Giltigt
        [DataRow("test@Lösenord2", true)] // Giltigt
        [DataRow("12345678!", false)] // Saknar stor bokstav
        [DataRow("Lösenordutansiffra!", false)] // Saknar siffra
        [DataRow("", false)] // Empty string
        [DataRow(null, false)] // Null string
        public void IsValid_VariousPasswords_ReturnsExpected(string password, bool expected)
        {
            // Act
            bool result = _validator.IsValidPassword(password);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}