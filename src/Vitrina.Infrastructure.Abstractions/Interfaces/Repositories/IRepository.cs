using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

/// <summary>
/// Interface for working with user domain objects.
/// </summary>
public interface IRepository<TEntity>
{
    /// <summary>
    /// Gets the user by ID.
    /// </summary>
    public Task<TEntity> GetByIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the user.
    /// </summary>
    public Task UpdateAsync(TEntity user, CancellationToken cancellationToken);
}
