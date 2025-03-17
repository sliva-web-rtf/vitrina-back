using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetCuratorById;

/// <inheritdoc />
public class GetCuratorByIdQueryHandler(IHandlerUserActions handler) : IRequestHandler<GetCuratorByIdQuery, CuratorDto>
{
    /// <inheritdoc />
    public async Task<CuratorDto> Handle(GetCuratorByIdQuery request, CancellationToken cancellationToken)
    {
        return await handler.GetUserById<CuratorDto>(request.CuratorId, cancellationToken);
    }
}
