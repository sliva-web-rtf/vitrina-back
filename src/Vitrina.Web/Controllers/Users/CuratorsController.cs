using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Domain.User;
using Vitrina.UseCases.UserProfile.GetUserById;

namespace Vitrina.Web.Controllers;

/// <summary>
/// A controller for working with curators.
/// </summary>
[ApiController]
[Authorize(Roles = "Curator")]
[Route("api/сurators")]
[ApiExplorerSettings(GroupName = "сurators")]
public class CuratorsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Getting curator profile data by Id.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("{curatorId:int}/profile")]
    public async Task<CuratorDto> GetUserProfileDataById([FromRoute] int сuratorId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery<CuratorDto>(сuratorId);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Edits curator profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("{curatorId:int}/profile/edit")]
    public async Task<CuratorDto> EditUserProfileById([FromRoute] int curatorId,
        [FromBody] JsonPatchDocument<CuratorDto> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand<CuratorDto, CuratorDto>(curatorId, patchDocument);
        return await mediator.Send(command, cancellationToken);
    }
}
