using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common.DTO;

public record EmailDto
{
    [EmailAddress]
    public string Email { get; init; }
}
