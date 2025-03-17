using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.User;

/// <summary>
/// Functionality for working with users.
/// </summary>
public interface IHandlerUserActions
{
    /// <summary>
    /// Getting a user by ID.
    /// </summary>
    public Task<TResultDto> GetUserById<TResultDto>(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// User update.
    /// </summary>
    public Task<TResultDto> UpdateById<TUpdateDto, TResultDto>(
        int userId,
        JsonPatchDocument<TUpdateDto> patchDocument,
        CancellationToken cancellationToken) where TUpdateDto : class;
}
