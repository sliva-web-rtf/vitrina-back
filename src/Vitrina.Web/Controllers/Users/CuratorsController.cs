using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser.GetCuratorById;
using Vitrina.UseCases.User.UpdateUser.UpdateCurator;

namespace Vitrina.Web.Controllers.Users;

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
    public async Task<CuratorDto> GetUserProfileDataById([FromRoute] int curatorId,
        CancellationToken cancellationToken)
    {
        var query = new GetCuratorByIdQuery(curatorId);
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
        var command = new UpdateCuratorCommand(curatorId, patchDocument);
        return await mediator.Send(command, cancellationToken);
    }
}
