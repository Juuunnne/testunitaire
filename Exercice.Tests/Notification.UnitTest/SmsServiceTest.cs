using FluentAssertions;
using Notification.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.UnitTest
{
    public class SmsServiceTest
    {
        private readonly SmsService _smsService;

        public SmsServiceTest()
        {
            _smsService = new SmsService();
        }

        // IsValidPhoneNumber Tests

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("123", false)]
        [InlineData("abcdefghij", false)]
        [InlineData("12345678901", false)]
        [InlineData("123456789a", false)]
        [InlineData("0123456789", true)]
        [InlineData("9876543210", true)]
        public void IsValidPhoneNumber_WithVariousInputs_ShouldReturnExpectedResult(string input, bool expected)
        {
            // Arrange & Act
            var result = _smsService.IsValidPhoneNumber(input);

            // Assert
            result.Should().Be(expected);
        }

        // SendSmsAsync Tests

        [Fact]
        public async Task SendSmsAsync_WithInvalidPhoneNumber_ShouldReturnFalse()
        {
            // Arrange & Act
            var result = await _smsService.SendSmsAsync("invalid123", "message");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SendSmsAsync_WithValidPhoneNumber_ShouldReturnTrue()
        {
            // Arrange & Act
            var result = await _smsService.SendSmsAsync("0123456789", "Hello");

            // Assert
            result.Should().BeTrue();
        }
    }
}
