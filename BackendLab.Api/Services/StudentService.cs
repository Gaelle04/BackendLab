namespace BackendLab.Api.Services;
using BackendLab.Api.Models;

public class StudentService : IStudentService
{
    private readonly List<OldStudent> _students = new()
    {
        new OldStudent(1, "gaelle", "gaelle@gmail.com"),
        new OldStudent(2, "chloe", "chloe@gmail.com"),
        new OldStudent(3, "chris", "chris@gmail.com"),
        new OldStudent(4, "John", "John@gmail.com")
    };

    public IEnumerable<OldStudent> GetAll() => _students;

    public OldStudent? GetById(long id) =>
        _students.FirstOrDefault(s => s.id == id);

    public IEnumerable<OldStudent> GetByValue(string value) =>
        _students.Where(s => s.name.Contains(value, StringComparison.OrdinalIgnoreCase));

    public OldStudent? Rename(long id, string newName)
    {
        var s = GetById(id);
        if (s is null) return null;
        s.name = newName;
        return s;
    }

    public OldStudent? Delete(long id)
    {
        var s = GetById(id);
        if (s is null) return null;
        _students.Remove(s);
        return s;
    }
}
