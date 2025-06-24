using Management.Model;
using Management.Service;

namespace Management.UnitTest;

public class StudentManagerTest
{
    private readonly StudentManager _manager = new ();
    
    [Fact]
    public void AddStudent_ValidStudent_AddsToCollection()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        
        // Act
        _manager.AddStudent(student);
        
        // Assert
        Assert.Single(_manager.Students);
        Assert.Contains(student, _manager.Students);
    }
    
    [Fact]
    public void AddMultipleStudent_ValidStudent_AddsToCollection()
    {
        // Arrange
        var student1 = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        var student2 = new Student { Id = 2, FirstName = "John", LastName = "Doe", Age = 20 };
        var student3 = new Student { Id = 3, FirstName = "John", LastName = "Doe", Age = 20 };
        
        // Act
        _manager.AddStudent(student1);
        _manager.AddStudent(student2);
        _manager.AddStudent(student3);
        
        // Assert
        Assert.Contains(student1, _manager.Students);
        Assert.Contains(student3, _manager.Students);
        Assert.Contains(student2, _manager.Students);
    }
    
    [Fact]
    public void AddMultipleStudent_WithSameIds_CantAdds2ToCollection()
    {
        // Arrange
        var student1 = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        var student2 = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        
        // Act && Assert
        _manager.AddStudent(student1);
        Assert.Throws<ArgumentException>(() => _manager.AddStudent(student2));
    }
    
    [Fact]
    public void GetStudentsByAge_ValidRange_ReturnsCorrectStudents()
    {
        // Arrange
        _manager.AddStudent(new Student
        {
            Id = 1,
            Age = 18,
            FirstName = "John",
            LastName = "Ynov"
        });
        _manager.AddStudent(new Student
        {
            Id = 2,
            Age = 20,
            FirstName = "Doe",
            LastName = "Ynov"
        });
        _manager.AddStudent(new Student
        {
            Id = 3,
            Age = 25,
            FirstName = "Paul",
            LastName = "Ynov"
        });
        
        // Act
        var result = _manager.GetStudentsByAge(18, 22);
        
        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, s => Assert.InRange(s.Age, 18, 22));
    }

}