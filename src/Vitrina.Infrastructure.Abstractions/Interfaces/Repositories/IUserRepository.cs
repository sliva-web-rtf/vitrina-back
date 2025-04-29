using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

/// <summary>
///     Interface for working with user domain objects.
/// </summary>
public interface IUserRepository
{
    public Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);

    public Task UpdateAsync(User entity, CancellationToken cancellationToken);

    public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
}
