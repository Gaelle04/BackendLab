using System.Globalization;
using BackendLab.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BackendLab.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    
    private static List<Student> _students = new()
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
    
    
   
    [HttpGet("by-id/{id}")]
    public ActionResult<IEnumerable<Student>> GetStudent([FromRoute]int id)
    {
        var student = _students.FirstOrDefault(s => s.id == id); 
        return student is null ? NotFound() : Ok(student);
    }
    
    [HttpGet("by-value")]
    public ActionResult<IEnumerable<Student>> GetStudentByValue([FromQuery]string value)
    {
        var results =_students.Where(s => s.name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList(); 
        return results.Count== 0 ? NotFound() : Ok(results);
    }
    
    [HttpGet("current-date")]
    public IActionResult GetCurrentDate()
    {
        
        var acceptLanguageHeader = Request.Headers["Accept-Language"].ToString();
        CultureInfo culture = CultureInfo.InvariantCulture;
        if (!string.IsNullOrEmpty(acceptLanguageHeader))
        {
            try
            {
                var languages = acceptLanguageHeader.Split(',')
                    .Select(l => l.Trim().Split(';')[0])
                    .ToList();
                if (languages.Any())
                {
                    culture = new CultureInfo(languages[0]); 
                }
            }
            catch (CultureNotFoundException)
            {
              
            }
        }

       
        var formattedDate = DateTime.Now.ToString("D", culture); 
        return Ok(new { CurrentDate = formattedDate, AcceptLanguageUsed = culture.Name });
    }

    [HttpPost("rename")]
    public ActionResult<Student> Rename([FromBody] UpdatedStudent body)
    {
        var student = _students.FirstOrDefault(s => s.id == body.id);
        if(student is null) 
            return NotFound();
        
        student.name = body.newName;
        return Ok(student);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        if (image is null || image.Length == 0)
            return BadRequest("No file was uploaded.");
        try
        {
            // Define the path within wwwroot
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            // Generate a safe filename 
            var extension = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}".ToLowerInvariant();
            var filePath = Path.Combine(uploadsFolder, fileName);
            
            // Save the image to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            
            // Return the URL or path to the uploaded image
            var imageUrl =$"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(new { Message = "Image uploaded successfully", ImageUrl = imageUrl });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id:long}")]
    public IActionResult DeleteById([FromRoute] long id)
    {
        if (id <= 0)
            return BadRequest("Id must be greater than or equal to zero");
        var student = _students.FirstOrDefault(s => s.id == id);

        if (student is null)
            return NotFound();
        
        _students.Remove(student);
        return Ok(student);

    }
}


