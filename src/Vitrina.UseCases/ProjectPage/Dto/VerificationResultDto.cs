using Vitrina.Domain.Project.Page;

namespace Vitrina.UseCases.ProjectPage.Dto;

public class VerificationResultDto
{
    public PageReadyStatusEnum NewPageStatus { get; init; }

    public string Message { get; init; }
}
