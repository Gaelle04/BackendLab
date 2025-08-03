using System.ComponentModel.DataAnnotations;

namespace BackendLab.Api.Models;

public class UpdatedStudent
{
    [Required(ErrorMessage = "Id is required")]
    public int id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string newName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string email { get; set; }
    

}