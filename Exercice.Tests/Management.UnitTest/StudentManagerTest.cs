using Management.Model;
using Management.Service;
using System.Collections.Generic;

namespace Management.UnitTest;

public class StudentManagerTest
{
    private readonly StudentManager _manager = new();

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

    [Fact]
    public void GetTopStudents_ReturnsCorrectStudents()
    {
        // Arrange
        _manager.AddStudent(
            new Student { Id = 1, Age = 18, FirstName = "K", LastName = "H", Grades = { 10, 12, 13, 1 } });
        _manager.AddStudent(
            new Student { Id = 2, Age = 19, FirstName = "F", LastName = "L", Grades = { 1, 15, 7, 15 } });
        _manager.AddStudent(
            new Student { Id = 3, Age = 18, FirstName = "D", LastName = "M", Grades = { 17, 12, 17, 15 } });
        _manager.AddStudent(
            new Student { Id = 4, Age = 21, FirstName = "R", LastName = "N", Grades = { 8, 6, 13, 15 } });
        _manager.AddStudent(
            new Student { Id = 5, Age = 20, FirstName = "Z", LastName = "P", Grades = { 7, 1, 13, 15 } });

        // Act 
        var result = _manager.GetTopStudents(1);

        // Assert 
        Assert.Single(result);
    }

    [Fact]
    public void GetStudentById_ExistingId_ReturnsCorrectStudent()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        _manager.AddStudent(student);

        // Act
        var result = _manager.GetStudentById(1);

        // Assert
        Assert.Equal(student, result);
    }

    [Fact]
    public void GetStudentById_NonExistingId_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _manager.GetStudentById(99));
    }

    [Fact]
    public void RemoveStudent_ExistingId_ReturnsTrueAndRemovesStudent()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        _manager.AddStudent(student);

        // Act
        var removed = _manager.RemoveStudent(1);

        // Assert
        Assert.True(removed);
        Assert.Empty(_manager.Students);
    }

    [Fact]
    public void RemoveStudent_NonExistingId_ReturnsFalse()
    {
        // Act
        var removed = _manager.RemoveStudent(99);

        // Assert
        Assert.False(removed);
    }

    [Fact]
    public void UpdateStudentGrades_ValidStudentId_UpdatesGrades()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20, Grades = { 10, 15 } };
        _manager.AddStudent(student);
        var newGrades = new List<int> { 18, 20 };

        // Act
        _manager.UpdateStudentGrades(1, newGrades);

        // Assert
        var updatedStudent = _manager.GetStudentById(1);
        Assert.Equal(newGrades, updatedStudent.Grades);
        Assert.Equal(19, updatedStudent.AverageGrade);
    }

    [Fact]
    public void UpdateStudentGrades_InvalidStudentId_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _manager.UpdateStudentGrades(99, new List<int> { 10 }));
    }
}