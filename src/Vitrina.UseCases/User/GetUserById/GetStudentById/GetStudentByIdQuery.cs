using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.UserProfile.GetUserById;

public record GetStudentByIdQuery(int UserId) : GetUserByIdQuery<StudentDto>(UserId);
