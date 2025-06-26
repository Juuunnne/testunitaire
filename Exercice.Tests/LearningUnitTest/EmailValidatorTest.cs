using Learning;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningUnitTest
{
    public class EmailValidatorTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void IsValidEmail_NullOrWhitespace_ShouldReturnsFalse(string input)
        {
            // Arrange
            var validator = new EmailValidator();

            // Act
            var result = validator.IsValidEmail(input);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidEmail_InvalidFormat_ShouldThrowsAndReturnsFalse()
        {
            // Arrange
            var input = "invalid-email";
            var validator = new EmailValidator();

            // Act
            var result = validator.IsValidEmail(input);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidEmail_ValidEmail_ShouldReturnsTrue()
        {
            // Arrange
            var input = "user.test@gmail.com";
            var validator = new EmailValidator();

            // Act
            var result = validator.IsValidEmail(input);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void IsValidEmailWithPattern_NullOrWhitespace_ShouldReturnsFalse(string input)
        {
            // Arrange
            var validator = new EmailValidator();

            // Act
            var result = validator.IsValidEmailWithPattern(input);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("missingatsign.com")]
        [InlineData("@nouser.com")]
        [InlineData("user@.com")]
        [InlineData("user@domain")]
        public void IsValidEmailWithPattern_InvalidFormat_ShouldReturnsFalse(string input)
        {
            // Arrange
            var validator = new EmailValidator();
            
            // Act
            var result = validator.IsValidEmailWithPattern(input);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("user.test@gmail.com")]
        [InlineData("user@domain.io")]
        public void IsValidEmailWithPattern_ValidEmail_ShouldReturnsTrue(string input)
        {
            // Arrange
            var validator = new EmailValidator();

            // Act
            var result = validator.IsValidEmailWithPattern(input);

            Assert.True(result);
        }
    }
}
