using BackendLab.Api.Models;
namespace BackendLab.Api.Services;


public interface IStudentService
{
    IEnumerable<OldStudent> GetAll();
    OldStudent GetById(long id);
    IEnumerable<OldStudent> GetByValue(string value);
    OldStudent? Rename(long id, string newName);
    OldStudent? Delete(long id);
}