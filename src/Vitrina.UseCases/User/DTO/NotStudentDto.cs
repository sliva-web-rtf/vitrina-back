using Vitrina.UseCases.User.DTO.AdditionalInformation;

namespace Vitrina.UseCases.User.DTO;

public record NotStudentDto : ResponceShortenedUserDto
{
    public AdditionalNotStudentInfo AdditionalInformation { get; init; } = new();
}
