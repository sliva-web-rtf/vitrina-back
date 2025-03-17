using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdateCurator;

/// <inheritdoc />
public class UpdateCuratorCommandHandler(IHandlerUserActions handler) : IRequestHandler<UpdateCuratorCommand, CuratorDto>
{
    /// <inheritdoc />
    public async Task<CuratorDto> Handle(UpdateCuratorCommand request, CancellationToken cancellationToken)
    {
        return await handler.UpdateById<CuratorDto, CuratorDto>(request.CuratorId, request.PatchDocument, cancellationToken);
    }
}
