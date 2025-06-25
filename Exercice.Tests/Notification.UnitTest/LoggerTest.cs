using FluentAssertions;
using Notification.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.UnitTest
{
    public class LoggerTest
    {
        private readonly Logger _logger;

        public LoggerTest()
        {
            _logger = new Logger();
        }

        [Theory]
        [InlineData("This is an info message", "[INFO]")]
        [InlineData("This is an error message", "[ERROR]")]
        [InlineData("This is a warning message", "[WARNING]")]
        public void Logger_LogsMessageWithCorrectPrefixAndColor(string message, string expectedPrefix)
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            if (expectedPrefix == "[INFO]") _logger.LogInfo(message);
            else if (expectedPrefix == "[ERROR]") _logger.LogError(message);
            else if (expectedPrefix == "[WARNING]") _logger.LogWarning(message);

            // Assert
            var consoleOutput = output.ToString();

            consoleOutput.Should().Contain(expectedPrefix);
            consoleOutput.Should().Contain(message);
        }
    }
}
