using System;
using System.Collections.Generic;

namespace BackendLab.Api.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public int? Age { get; set; }
}
