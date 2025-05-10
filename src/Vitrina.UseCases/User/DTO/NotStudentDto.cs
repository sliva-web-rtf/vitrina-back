using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public class NotStudentDto : UserDto
{
    public AdditionalNotStudentInfo AdditionalInformation { get; init; } = new();
}
