namespace BackendLab.Api.Services;
using BackendLab.Api.Models;

public class StudentService : IStudentService
{
    private readonly List<Student> _students = new()
    {
        new Student(1, "gaelle", "gaelle@gmail.com"),
        new Student(2, "chloe", "chloe@gmail.com"),
        new Student(3, "chris", "chris@gmail.com"),
        new Student(4, "John", "John@gmail.com")
    };

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(long id) =>
        _students.FirstOrDefault(s => s.id == id);

    public IEnumerable<Student> GetByValue(string value) =>
        _students.Where(s => s.name.Contains(value, StringComparison.OrdinalIgnoreCase));

    public Student? Rename(long id, string newName)
    {
        var s = GetById(id);
        if (s is null) return null;
        s.name = newName;
        return s;
    }

    public Student? Delete(long id)
    {
        var s = GetById(id);
        if (s is null) return null;
        _students.Remove(s);
        return s;
    }
}
