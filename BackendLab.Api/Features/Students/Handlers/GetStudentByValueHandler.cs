using BackendLab.Api.Features.Students.Queries;
using BackendLab.Api.Models;
using BackendLab.Api.Services;
using MediatR;

namespace BackendLab.Api.Features.Students.Handlers;

public class GetStudentByValueHandler : IRequestHandler<GetStudentByValue, IEnumerable<Student>>
{
    private readonly IStudentService _studentService;


    public GetStudentByValueHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public Task<IEnumerable<Student>> Handle(GetStudentByValue request, CancellationToken cancellationToken)
    {
        var results = _studentService.GetByValue(request.Value);
        return Task.FromResult<IEnumerable<Student>>(results);
    }
}
