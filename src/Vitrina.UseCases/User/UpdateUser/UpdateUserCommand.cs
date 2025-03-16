using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.Domain.User;

/// <summary>
/// The command to update user profile data.
/// </summary>
public record UpdateUserCommand<TUpdateDto, TResultDto>(int UserId, JsonPatchDocument<TUpdateDto> PatchDocument)
    : IRequest<TResultDto> where TUpdateDto : class;
