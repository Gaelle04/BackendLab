using BackendLab.Api.Features.Students.Queries;
using BackendLab.Api.Models;
using BackendLab.Api.Services;
using MediatR;

namespace BackendLab.Api.Features.Students.Handlers;

public class GetStudentByIdHandler: IRequestHandler<GetStudentById, OldStudent?>
{
    private readonly IStudentService _studentService;

    public GetStudentByIdHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public Task<OldStudent?> Handle(GetStudentById request, CancellationToken cancellationToken)
    {
        var student = _studentService.GetById(request.Id);
        return Task.FromResult(student);
    }
}