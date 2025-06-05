using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public record StudentDto : UserDtoBase
{
    public AdditionalStudentInfo AdditionalInformation { get; init; } = new();
}
