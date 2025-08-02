using System.ComponentModel.DataAnnotations;

namespace BackendLab.Api.Models;

public class Student
{
    [Required(ErrorMessage ="Please enter your id")]

    public long id { get; set; }
    [Required(ErrorMessage ="Please enter your name")]
    [MinLength(3, ErrorMessage ="Your name must be at least 3 characters")]
    public string name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage ="Please enter your email")]
    public string email { get; set; }

    public Student(long id, string name, string email)
    {
        id = this.id;
        name = this.name;
        email = this.email;
        
    }
}