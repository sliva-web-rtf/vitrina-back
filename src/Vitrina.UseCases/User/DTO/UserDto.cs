using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public record UserDto : UserDtoBase
{
    public AdditionalUserInfo AdditionalInformation { get; init; } = new();
}
