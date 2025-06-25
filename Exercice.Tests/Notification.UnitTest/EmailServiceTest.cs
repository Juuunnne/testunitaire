using FluentAssertions;
using Moq;
using Notification.Contracts;
using Notification.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.UnitTest
{
    public class EmailServiceTest
    {
        private readonly EmailService _emailService;

        public EmailServiceTest()
        {
            _emailService = new EmailService();
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("    ", false)]
        [InlineData("ceciestmonmail", false)]
        [InlineData("test@gmail.com", true)]
        [InlineData("TEST@GMAIL.COM", true)]
        [InlineData("TEST.user+tag@gmail.com", true)]
        [InlineData("test.us ser@gmail.com", false)]
        public void IsValidEmail_WithVariousInputs_ShouldReturnExpectedResult(string email, bool expected)
        {
            // Arrange
            var result = _emailService.IsValidEmail(email);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public async Task SendEmailAsync_WithInvalidEmail_ShouldReturnFalse()
        {
            // Arrange
            var invalidEmail = "invalid-email";

            // Act
            var result = await _emailService.SendEmailAsync(invalidEmail, "subject", "body");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendEmailAsync_WithValidEmail_ShouldReturnTrue()
        {
            // Arrange
            var validEmail = "user@test.com";

            // Act
            var result = await _emailService.SendEmailAsync(validEmail, "subject", "body");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void SendWelcomeEmail_WithInvalidEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidEmail = "invalid-email";

            // Act
            Action result = () => _emailService.SendWelcomeEmail(invalidEmail, "John Doe");

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Invalid email address");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void SendWelcomeEmail_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var validEmail = "user@test.com";

            // Act
            Action result = () => _emailService.SendWelcomeEmail(validEmail, invalidName);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty");
        }

        [Fact]
        public void SendWelcomeEmail_WithValidEmailAndName_ShouldNotThrow()
        {
            // Arrange
            var validEmail = "user@test.com";
            var validName = "John Doe";

            // Act
            Action result = () => _emailService.SendWelcomeEmail(validEmail, validName);

            // Assert
            result.Should().NotThrow();
        }
    }
}
