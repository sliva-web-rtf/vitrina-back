using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public abstract record UpdateUserDtoBase : UserDtoBase
{
    public AdditionalUserInfo AdditionalInformation { get; init; } = new();
}
