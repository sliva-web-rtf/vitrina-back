using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public record StudentDtoBase : UserDtoBase
{
    public AdditionalStudentInfo AdditionalInformation { get; init; } = new();
}
