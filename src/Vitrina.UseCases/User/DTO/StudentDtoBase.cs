using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public abstract record StudentDtoBase : UserDtoBase
{
    public AdditionalStudentInfo AdditionalInformation { get; init; } = new();
}
