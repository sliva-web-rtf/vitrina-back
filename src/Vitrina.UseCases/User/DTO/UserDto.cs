using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public record UpdateUserDto : UserDto
{
    public AdditionalUserInfo AdditionalInformation { get; init; } = new();
}
