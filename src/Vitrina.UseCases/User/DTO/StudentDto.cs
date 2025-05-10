using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public class StudentDto : UserDto
{
    public AdditionalStudentInfo AdditionalInformation { get; init; } = new();
}
