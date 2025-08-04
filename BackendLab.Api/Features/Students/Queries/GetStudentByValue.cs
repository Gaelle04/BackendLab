using BackendLab.Api.Models;
using MediatR;

namespace BackendLab.Api.Features.Students.Queries;

public class GetStudentByValue : IRequest<IEnumerable<OldStudent>>
{
   public string Value { get; }

   public GetStudentByValue(string value)
   {
      Value = value;
   }
    
   
}