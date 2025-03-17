using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdateStudent;

/// <summary>
/// The command to update student profile data.
/// </summary>
public record UpdateStudentCommand(int StudentId, JsonPatchDocument<UpdateStudentDto> PatchDocument) : IRequest<StudentDto>;
