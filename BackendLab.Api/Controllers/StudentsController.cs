using System.Globalization;
using BackendLab.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using BackendLab.Api.Features.Students.Queries;
using BackendLab.Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackendLab.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    
 private readonly IStudentService _students;
 private readonly BackendLabDbContextCodeFirst _context;

 public StudentsController(IStudentService students, IMediator mediator, BackendLabDbContextCodeFirst context)
 {
     _students = students;
     _mediator = mediator;
     _context = context;
 } 
 
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _context.Students
            .OrderBy(s=>s.Name)
            .ToListAsync();
        return Ok(students);
    }
    
    private readonly IMediator _mediator;
   
   
    [HttpGet("by-id/{id}")]
    public async Task<ActionResult> GetStudent([FromRoute]long id)
    {
        
        if(id <=0)
            throw new ArgumentOutOfRangeException(nameof(id), "id must be greater than zero");
        
        var student =  await _context.Students.FirstOrDefaultAsync(s => s.Id == id); 
        return student is null ? NotFound() : Ok(student);
    }
    
    [HttpGet("by-value")]
    public async Task<ActionResult> GetStudentByValue([FromQuery]string value)
    {
        if(string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value), "Query 'value' is required.");
        
        var results = await _context.Students
            .Where(s => s.Name.Contains(value))
            .ToListAsync();
        return Ok(results);
    }
    
    [HttpGet("current-date")]
    public IActionResult GetCurrentDate()
    {
        
        var acceptLanguageHeader = Request.Headers["Accept-Language"].ToString();
        CultureInfo culture = CultureInfo.InvariantCulture;

        try
        {
            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                var languages = acceptLanguageHeader.Split(',')
                    .Select(l => l.Trim().Split(';')[0])
                    .ToList();
                if (languages.Any())
                {

                    culture = new CultureInfo(languages[0]);


                }
            }

        }
        catch (CultureNotFoundException)
        {

        }
        finally
        {
                Console.WriteLine($"Using culture: {culture.Name}");
        }
        
        var formattedDate = DateTime.Now.ToString("D", culture); 
        return Ok(new { CurrentDate = formattedDate, AcceptLanguageUsed = culture.Name });
    }

    [HttpPost("rename")]
    public ActionResult<OldStudent> Rename([FromBody] UpdatedStudent body)
    {
        
        if(body == null)
            throw new  ArgumentNullException(nameof(body), "Body is required"); 
        if(body.id < 0)
            throw new ArgumentOutOfRangeException(nameof(body), "id must be greater than zero");
        
        var student = _students.Rename(body.id, body.newName);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        
       if(image ==null)
           throw new  ArgumentNullException(nameof(image), "Image is required");
       if (image.Length == 0)
           throw new ArgumentOutOfRangeException(nameof(image), "Upload file is empty"); 
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
    public async Task<IActionResult> DeleteById([FromRoute] long id)
    {
       if(id <= 0)
           throw new ArgumentOutOfRangeException(nameof(id), "id must be greater than zero");
     
         
       var studentDeleted = await _context.Students.FindAsync(id);
       if (studentDeleted is null)
           return NotFound();
       _context.Students.Remove(studentDeleted);
       await _context.SaveChangesAsync();
       return NoContent();



    }
}


