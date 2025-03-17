using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetStudentById;

/// <summary>
/// Query to get a student by id.
/// </summary>
public record GetStudentByIdQuery(int StudentId) : IRequest<StudentDto>;
