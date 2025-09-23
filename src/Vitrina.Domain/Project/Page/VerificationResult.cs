namespace Vitrina.Domain.Project.Page;

public class VerificationResult
{
    public Guid Id { get; init; }

    public Guid PageId { get; init; }

    public virtual ProjectPage Page { get; init; }

    public PageReadyStatusEnum NewPageStatus { get; init; }

    public string Message { get; init; }

    public DateTime Date { get; init; }

    public required int ModeratorId { get; init; }
}
