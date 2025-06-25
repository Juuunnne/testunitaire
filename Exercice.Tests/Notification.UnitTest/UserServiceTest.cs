using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Model;
using Notification.Services;

namespace Notification.UnitTest;

public class UserServiceTest
{
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTest()
    {
        _emailServiceMock = new Mock<IEmailService>();
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    [Trait("Category", "User")]
    [Fact]
    public void CreateUser_WhenUserDoesNotExist_ShouldCreateUserAndSendEmail()
    {
        // Arrange
        string userName = "Test User";
        string userEmail = "user.test@gmail.com";

        _userRepositoryMock.Setup(repo => repo.Exists(userEmail)).Returns(false);
        _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Verifiable();

        _emailServiceMock.Setup(e => e.IsValidEmail(userEmail)).Returns(true);
        _emailServiceMock.Setup(email => email.SendWelcomeEmail(userEmail, userName)).Verifiable();

        var userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);

        // Act
        var user = userService.CreateUser(userName, userEmail);

        // Assert
        user.Should().NotBeNull();
        user.Name.Should().Be(userName);
        user.Email.Should().Be(userEmail);

        _userRepositoryMock.Verify(repo => repo.Exists(userEmail), Times.Once);
        _userRepositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once);
        _emailServiceMock.Verify(email => email.SendWelcomeEmail(userEmail, userName), Times.Once);
    }

    [Trait("Category", "User")]
    [Fact]
    public void CreateUser_WithInvalidEmail_ShouldThrowException()
    {
        // Arrange
        var userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);
        string invalidEmail = "invalid-email";

        // Act & Assert
        Assert.Throws<FormatException>(() => userService.CreateUser("Test User", invalidEmail));
    }

    [Trait("Category", "User")]
    [Fact]
    public void CreateUser_WithEmptyName_ShouldThrowException()
    {
        // Arrange
        string userName = "";
        string userEmail = "user.test@gmail.com";

        _userRepositoryMock.Setup(repo => repo.Exists(userEmail)).Returns(false);
        _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Verifiable();

        _emailServiceMock.Setup(e => e.IsValidEmail(userEmail)).Returns(true);

        var userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => userService.CreateUser(userName, userEmail));

        exception.Message.Should().Be("Name cannot be empty");
        exception.ParamName.Should().Be(null);
        _userRepositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Never);
    }

    [Trait("Category", "User")]
    [Fact]
    public void CreateUser_WhenUserExists_ShouldThrowException()
    {
        // Arrange
        var userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);
        string userName = "Test User";
        string userEmail = "user.test@gmail.com";

        _userRepositoryMock.Setup(repo => repo.Exists(userEmail)).Returns(true);
        _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Verifiable();

        _emailServiceMock.Setup(e => e.IsValidEmail(userEmail)).Returns(true);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => userService.CreateUser(userName, userEmail));

        exception.Message.Should().Be("User already exists");

        _userRepositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Never);
        _emailServiceMock.Verify(email => email.SendWelcomeEmail(userEmail, userName), Times.Never);
    }

    [Trait("Category", "User")]
    [Fact]
    public void CreateUser_WithValidData_ShouldCaptureUserDetails()
    {
        // Arrange
        string userName = "Captured User";
        string userEmail = "captured@example.com";

        User? capturedUser = null;

        _userRepositoryMock.Setup(repo => repo.Exists(userEmail)).Returns(false);
        _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Callback<User>(u => capturedUser = u);

        _emailServiceMock.Setup(e => e.IsValidEmail(userEmail)).Returns(true);
        _emailServiceMock.Setup(e => e.SendWelcomeEmail(userEmail, userName)).Verifiable();

        var userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);

        // Act
        var createdUser = userService.CreateUser(userName, userEmail);

        // Assert
        capturedUser.Should().NotBeNull();
        capturedUser!.Name.Should().Be(userName);
        capturedUser.Email.Should().MatchRegex(".+@.+\\..+");

        createdUser.Should().BeEquivalentTo(capturedUser);
    }
}