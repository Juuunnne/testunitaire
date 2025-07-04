using Learning;

namespace LearningUnitTest;

public class PersonTest
{
    [Fact]
    public void GetFullName_WithFirstAndLastName_ReturnsFormattedName()
    {
        // Arrange
        Person person = new Person { FirstName = "John", LastName = "Doe" };
        
        // Act 
        string fullName = person.GetFullName();
        
        // Assert
        Assert.Equal("John Doe", fullName);
    }
    
    [Fact]
    public void GetFullName_WithLastName_ReturnsFormattedName()
    {
        // Arrange
        Person person = new Person { FirstName = "", LastName = "Doe" };
        
        // Act 
        string fullName = person.GetFullName();
        
        // Assert
        Assert.Equal("Doe", fullName);
    }
    
    [Fact]
    public void GetFullName_WithFirstName_ReturnsFormattedName()
    {
        // Arrange
        Person person = new Person { FirstName = "John", LastName = "" };
        
        // Act 
        string fullName = person.GetFullName();
        
        // Assert
        Assert.Equal("John", fullName);
    }
    
    [Fact]
    public void GetFullName_WithNoNames_ReturnsFormattedName()
    {
        // Arrange
        Person person = new Person { FirstName = "", LastName = "" };
        
        // Act 
        string fullName = person.GetFullName();
        
        // Assert
        Assert.Equal("", fullName);
    }
    
    
    [Fact]
    public void GetFullName_WithSpace_ReturnsFormattedName()
    {
        // Arrange
        Person person = new Person { FirstName = "John  ", LastName = " Doe    " };
        
        // Act 
        string fullName = person.GetFullName();
        
        // Assert
        Assert.Equal("John Doe", fullName);
    }

    [Theory]
    [InlineData(17, false)]
    [InlineData(18, true)]
    [InlineData(25, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsAdult_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        // arrange 
        Person person = new Person { Age = age };
        
        // Act
        bool isAdult = person.IsAdult();
        
        // Assert
        Assert.Equal(isAdult, expected);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(25, true)]
    [InlineData(150, true)]
    [InlineData(151, false)]
    public void IsValidAge_DifferentAges_ReturnsCorrectResult(int age, bool expected)
    {
        // arrange 
        Person person = new Person { Age = age };
        
        // Act
        bool isValidAge = person.IsValidAge();
        
        // Assert
        Assert.Equal(isValidAge, expected);
    }
}