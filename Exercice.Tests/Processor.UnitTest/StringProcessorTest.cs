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
    [InlineData("hello", "Hello")]
    [InlineData("Hello", "Hello")]
    [InlineData("Test avec une phrase", "Test avec une phrase")]
    [InlineData("Ynov123", "Ynov123")]
    [InlineData("123", "123")]
    [InlineData("  leading and trailing spaces  ", "Leading and trailing spaces")]
    public void Capitalize_VariousInputs_ReturnsCapitalizedString(string input, string expected)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act
        string result = processor.Capitalize(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Trait("Category", "String_Processing")]
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Capitalize_NullOrWhitespaceInput_ThrowsArgumentNullException(string input)
    {
        // Arrange
        var processor = new StringProcessor();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => processor.Capitalize(input));
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
    [InlineData("   Ta\tYnov va à ; lyon\nMaster\nClasse , 107\tThéo\tAlexandre", 10)]
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