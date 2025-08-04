using BackendLab.Api.Features.Students.Queries;
using BackendLab.Api.Models;
using BackendLab.Api.Services;
using MediatR;

namespace BackendLab.Api.Features.Students.Handlers;

public class GetStudentByValueHandler : IRequestHandler<GetStudentByValue, IEnumerable<OldStudent>>
{
    private readonly IStudentService _studentService;


    public GetStudentByValueHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public Task<IEnumerable<OldStudent>> Handle(GetStudentByValue request, CancellationToken cancellationToken)
    {
        var results = _studentService.GetByValue(request.Value);
        return Task.FromResult<IEnumerable<OldStudent>>(results);
    }
}
