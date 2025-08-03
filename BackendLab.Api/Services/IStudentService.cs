using BackendLab.Api.Models;
namespace BackendLab.Api.Services;


public interface IStudentService
{
    IEnumerable<Student> GetAll();
    Student GetById(long id);
    IEnumerable<Student> GetByValue(string value);
    Student? Rename(long id, string newName);
    Student? Delete(long id);
}