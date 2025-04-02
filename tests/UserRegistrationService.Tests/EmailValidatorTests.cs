using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration;

namespace UserRegistrationService.Tests
{
    [TestClass]
    public class EmailValidatorTests
    {
        private EmailValidator _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            // Skapa en instans av EmailValidator
            _validator = new EmailValidator();
        }

        [TestMethod]
        [DataRow("user@example.com", true)] // Testa med en giltig e-postadress
        [DataRow("user.admin@example.com", true)] // Testa med en giltig e-postadress med domän
        [DataRow("user@example.se", true)] // Testa med en giltig e-postadress med annan domän
        [DataRow("user@example", false)] // (saknar domän)
        [DataRow("@example.com", false)] // (saknar användarnamn)
        [DataRow("user@.com", false)] // (saknar domän)
        [DataRow("userexample.com", false)] // (saknar @)
        [DataRow("", false)]  
        [DataRow(null, false)]  
        public void IsValid_VariousEmails_ReturnsExpected(string email, bool expected)
        {
            // Act
            bool result = _validator.IsValidEmail(email);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}