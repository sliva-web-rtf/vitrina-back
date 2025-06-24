using MediatR;
using Saritasa.Tools.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Auth.GetUserById;

/// <summary>
/// Handler for <see cref="GetUserByIdQuery" />.
/// </summary>
internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    public GetUserByIdQueryHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Teammates.GetAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        return new UserDetailsDto
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
}
