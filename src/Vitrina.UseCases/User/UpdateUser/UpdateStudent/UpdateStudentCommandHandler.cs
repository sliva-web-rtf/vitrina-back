using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdateStudent;

/// <inheritdoc />
public class UpdateStudentCommandHandler(IHandlerUserActions handler) : IRequestHandler<UpdateStudentCommand, StudentDto>
{
    /// <inheritdoc />
    public async Task<StudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        return await handler.UpdateById<UpdateStudentDto, StudentDto>(
            request.StudentId,
            request.PatchDocument,
            cancellationToken);
    }
}
