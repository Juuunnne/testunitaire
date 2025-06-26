using Management.Model;

namespace Management.Service;

public class StudentManager : IStudentManager
{
    private readonly List<Student> _students = new ();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    
    public void AddStudent(Student student)
    {
        if (_students.Any(s => s.Id == student.Id))
            throw new ArgumentException("Student already exists");
        _students.Add(student);
    }

    public Student GetStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id) ?? throw new InvalidOperationException();
    }

    public List<Student> GetStudentsByAge(int minAge, int maxAge)
    {
        return _students.FindAll(s => s.Age >= minAge && s.Age <= maxAge) ?? throw new InvalidOperationException();
    }

    public List<Student> GetTopStudents(int count)
    {
        return _students.OrderByDescending(s => s.AverageGrade).Take(count).ToList();
    }

    public bool RemoveStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        return student != null && _students.Remove(student);
    }

    public void UpdateStudentGrades(int studentId, List<int> newGrades)
    {
        var student = GetStudentById(studentId) ?? throw new ArgumentException("Student not found");
        student.Grades = newGrades ?? [];
    }
}