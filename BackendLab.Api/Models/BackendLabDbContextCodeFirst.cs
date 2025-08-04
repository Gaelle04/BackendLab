using Microsoft.EntityFrameworkCore;

namespace BackendLab.Api.Models;

public class BackendLabDbContextCodeFirst : DbContext
{
    public DbSet<Student> Students  { get; set; }
    public BackendLabDbContextCodeFirst(DbContextOptions<BackendLabDbContextCodeFirst> options) : base(options)
    {
    }

    
    
 
}