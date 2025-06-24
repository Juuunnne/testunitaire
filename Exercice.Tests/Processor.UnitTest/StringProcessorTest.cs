using Processor.Services;

namespace Processor.UnitTest;

public class StringProcessorTest
{
    [Theory]
    [InlineData("hello", "olleh")]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("12345", "54321")]
    public void Reverse_VariousInputs_ReturnsReversedString(string input, string expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        string result = processor.Reverse(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("radar", true)]
    [InlineData("hello", false)]
    [InlineData("A man a plan a canal Panama", true)]
    [InlineData("", true)]
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(string input, bool expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        bool isPalindrome = processor.IsPalindrome(input);
        
        // Assert
        Assert.Equal(expected, isPalindrome);
    }
    
    [Theory]
    [InlineData("hello", "HELLO")]
    [InlineData("Hello", "HELLO")]
    [InlineData("", "")]
    public void ToUpper_VariousInputs_ReturnsCorrectResult(string input, string expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        processor.Capitalize(input);
        
        // Assert
        Assert.Equal(expected, input);
    }

    [Fact]
    public void ToUpper_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => processor.Capitalize(null));
    }
    
    [Fact]
    public void ToUpper_NumberInput_ThrowsArgumentException()
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => processor.Capitalize("123"));
    }


}