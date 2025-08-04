using System.ComponentModel.DataAnnotations;

namespace BackendLab.Api.Models;

public class OldStudent
{
    [Required(ErrorMessage ="Please enter your id")]

    public long id { get; set; }
    [Required(ErrorMessage ="Please enter your name")]
    [MinLength(3, ErrorMessage ="Your name must be at least 3 characters")]
    public string name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage ="Please enter your email")]
    public string email { get; set; }

    public OldStudent(long id, string name, string email)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        
    }
}