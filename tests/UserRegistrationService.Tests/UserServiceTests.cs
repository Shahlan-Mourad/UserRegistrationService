using Moq;
using UserRegistration.Services;
using UserRegistrationService.Models;


namespace UserRegistration.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _service = null!;

        // Deklarera mockade validerare som privata fält
        private Mock<IUsernameValidator> _usernameValidatorMock = null!;
        private Mock<IEmailValidator> _emailValidatorMock = null!;
        private Mock<IPasswordValidator> _passwordValidatorMock = null!;

        [TestInitialize]
        public void Setup()
        {
            // Skapa mockade validerare
            _usernameValidatorMock = new Mock<IUsernameValidator>();
            _emailValidatorMock = new Mock<IEmailValidator>();
            _passwordValidatorMock = new Mock<IPasswordValidator>();

            // Injicera mockarna i UserService
            _service = new UserService(
                _usernameValidatorMock.Object,
                _emailValidatorMock.Object,
                _passwordValidatorMock.Object);
        }
        private User CreateMockUser(string username = "testuser", string email = "testuser@example.com", string password = "testpassword1!")
        {
            return new User(username, email, password);
        }

        private void SetupValidators(bool isUsernameValid, bool isEmailValid, bool isPasswordValid)
        {
            _usernameValidatorMock.Setup(v => v.IsValidName(It.IsAny<string>())).Returns(isUsernameValid);
            _emailValidatorMock.Setup(v => v.IsValidEmail(It.IsAny<string>())).Returns(isEmailValid);
            _passwordValidatorMock.Setup(v => v.IsValidPassword(It.IsAny<string>())).Returns(isPasswordValid);
        }

        [TestMethod]
        public void RegisterUser_ValidUser_ReturnsSuccess()
        {
            // Arrange
            var user = CreateMockUser();
            SetupValidators(true, true, true);
            
            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("testuser", result.Username);
            Assert.AreEqual("Användaren har registrerats", result.Message);
        }

        [TestMethod]
        public void RegisterUser_InvalidUsername_ReturnsFailure()
        {
            // Arrange
            var user = CreateMockUser(username: "tes");
            SetupValidators(false, true, true);

            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Användarnamn måste vara 5-20 alfanumeriska tecken", result.Message);
        }

        [TestMethod]
        public void RegisterUser_DuplicateUsername_ReturnsFailure()
        {
            // Arrange
            var user1 = CreateMockUser();
            var user2 = CreateMockUser(email : "test2@example.com");
            SetupValidators(true, true, true);

            // Act
            _service.RegisterUser(user1);
            var result = _service.RegisterUser(user2);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Användarnamnet är redan upptaget", result.Message);
        }

        [TestMethod]
        public void RegisterUser_InvalidPassword_ReturnsFailure()
        {
            // Arrange
            var user = CreateMockUser(password: "testpassword");
            SetupValidators(true, true, false);
          
            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lösenordet måste vara minst 8 tecken och innehålla minst ett specialtecken, en stor bokstav och en siffra", result.Message);
        }

        [TestMethod]
        public void RegisterUser_InvalidEmail_ReturnsFailure()
        {
            // Arrange
            var user = CreateMockUser(email: "ogiltig-email");
            SetupValidators(true, false, true);
           
            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Ogiltig e-postadress", result.Message);
        }

        [TestMethod]
        public void RegisterUser_MultipleValidUsers_AllRegisteredSuccessfully()
        {
            // Arrange
            var user1 = CreateMockUser(username: "user1", email: "user1@example.com");
            var user2 = CreateMockUser(username: "user2", email: "user2@example.com");
            SetupValidators(true, true, true);

            // Act
            var result1 = _service.RegisterUser(user1);
            var result2 = _service.RegisterUser(user2);

            // Assert
            Assert.IsTrue(result1.IsSuccess);
            Assert.IsTrue(result2.IsSuccess);
            Assert.AreEqual(2, _service.GetAllUsers().Count());
        }

        [TestMethod]
        public void RegisterUser_EmptyUsername_ReturnsFailure()
        {
            // Arrange
            var user = CreateMockUser(username: "");
            SetupValidators(false, true, true);
           
            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Användarnamn måste vara 5-20 alfanumeriska tecken", result.Message);
        }

        [TestMethod]
        public void RegisterUser_EmptyPassword_ReturnsFailure()
        {
            // Arrange
            var user = CreateMockUser(password: "");
            SetupValidators(true, true, false);
            
            // Act
            var result = _service.RegisterUser(user);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lösenordet måste vara minst 8 tecken och innehålla minst ett specialtecken, en stor bokstav och en siffra", result.Message);
        }
    }
}