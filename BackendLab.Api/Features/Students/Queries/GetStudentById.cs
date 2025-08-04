namespace BackendLab.Api.Features.Students.Queries;
using BackendLab.Api.Models;
using MediatR;

public class GetStudentById: IRequest<Student?>
{
    public long Id { get; set; }

    public GetStudentById(long id)
    {
        Id = id;
    }
    
}