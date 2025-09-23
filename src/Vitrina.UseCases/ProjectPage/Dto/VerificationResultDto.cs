using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vitrina.Domain.Project.Page;

namespace Vitrina.UseCases.ProjectPage.Dto;

public class VerificationResultDto
{
    required public PageReadyStatusEnum NewPageStatus { get; init; }

    required public string Message { get; init; }

    [BindNever]
    public int ModeratorId { get; set; }
}
