using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration;

namespace UserRegistrationService.Tests
{
    [TestClass]
    public class UserValidatorTests
    {
        private UsernameValidator _validator = null!;

        [TestInitialize]
        public void Setup()
        {
            // Skapa en instans av UsernameValidator
            _validator = new UsernameValidator();
        }

        [TestMethod]
        [DataRow("sh", false)] // För kort
        [DataRow("Shalan12345678901234567890", false)] // För lång
        [DataRow("Shahlan_Mourad", false)] // Innehåller understreck tecken
        [DataRow("Shahlan Mourad", false)] // Innehåller mellanslag
        [DataRow("Shahlan1", true)] // Giltigt
        [DataRow("ShahlanMo12", true)] // Giltigt
        [DataRow("", false)] // Empty string
        [DataRow(null, false)] // Null string
        public void IsValidUsername_VariousInputs_ReturnsExpectedResult(string username, bool expected)
        {
            // Act
            bool result = _validator.IsValidName(username);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}