using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public class UpdateUserDto : UserDto
{
    public AdditionalUserInfo AdditionalInformation { get; init; }
}
