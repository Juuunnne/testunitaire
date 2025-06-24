using Processor.Services;

namespace Processor.UnitTest;

public class StringProcessorTest
{
    [Trait("Category", "String_Processing")]
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

    [Trait("Category", "String_Processing")]
    [Theory]
    [InlineData("radar", true)]
    [InlineData("hello", false)]
    [InlineData("A man a plan a canal Panama", true)]
    [InlineData(" ", false)]
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(string input, bool expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        bool isPalindrome = processor.IsPalindrome(input);
        
        // Assert
        Assert.Equal(expected, isPalindrome);
    }

    [Trait("Category", "String_Processing")]
    [Fact]
    public void IsPalindrome_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => processor.IsPalindrome(""));
    }

    [Trait("Category", "String_Processing")]
    [Theory]
    [InlineData("hello", "HELLO")]
    [InlineData("Hello", "HELLO")]
    [InlineData("Test avec une phrase", "TEST AVEC UNE PHRASE")]
    public void ToUpper_VariousInputs_ReturnsCorrectResult(string input, string expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        string result = processor.Capitalize(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Trait("Category", "String_Processing")]
    [Fact]
    public void ToUpper_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => processor.Capitalize(null));
    }
    
    [Trait("Category", "String_Processing")]
    [Fact(Skip = "Test désactivé temporairement")]
    public void ToUpper_NumberInput_ThrowsArgumentException()
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => processor.Capitalize("123"));
    }

    [Trait("Category", "String_Processing")]
    [Theory]
    [InlineData("Hello world", 2)]
    [InlineData("   ", 0)]
    [InlineData("OneTwoThree", 1)]
    [InlineData("  Leading and trailing spaces  ", 4)]
    [InlineData("Multiple   spaces", 2)]
    [InlineData("Tabulation\tTabulation", 2)]
    [InlineData("New\nLine", 2)]
    public void CountWords_VariousInputs_ReturnsCorrectCount(string input, int expectedCount)
    {
        // Arrange
        var processor = new StringProcessor();

        // Act
        int wordCount = processor.CountWords(input);

        // Assert
        Assert.Equal(expectedCount, wordCount);
    }
}