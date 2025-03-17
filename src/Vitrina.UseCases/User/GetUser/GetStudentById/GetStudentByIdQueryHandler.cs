using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetStudentById;

/// <inheritdoc />
public class GetStudentByIdQueryHandler(IHandlerUserActions handler) : IRequestHandler<GetStudentByIdQuery, StudentDto>
{
    /// <inheritdoc />
    public async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return await handler.GetUserById<StudentDto>(request.StudentId, cancellationToken);
    }
}
