using BackendLab.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BackendLab;

namespace BackendLab.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    
    private List<Student> _students = new()
    {
        new Student(1, "gaelle", "gaelle@gmail.com"),
        new Student(2, "chloe", "chloe@gmail.com"),
        new Student(3, "chris", "chris@gmail.com"),
        new Student(4, "John", "John@gmail.com")
        
    };
    
    [HttpGet()]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return Ok(_students);
    }
    
    
   
    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.id == id); 
        return student is null ? NotFound() : Ok(student);
    }
}
