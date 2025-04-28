using MediatR;

namespace Vitrina.UseCases.Project.GetPeriods;

/// <summary>
///     Get periods.
/// </summary>
public class GetPeriodsQuery : IRequest<ICollection<string>>
{
}
