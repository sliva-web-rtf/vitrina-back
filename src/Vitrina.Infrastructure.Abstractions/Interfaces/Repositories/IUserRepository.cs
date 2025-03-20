using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

/// <summary>
/// Interface for working with user domain objects.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets the user by ID.
    /// </summary>
    public Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the user.
    /// </summary>
    public Task UpdateAsync(User user, CancellationToken cancellationToken);
}
