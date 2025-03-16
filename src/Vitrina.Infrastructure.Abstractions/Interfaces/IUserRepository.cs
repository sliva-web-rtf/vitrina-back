using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken);

    public Task UpdateAsync(User user, CancellationToken cancellationToken);
}
