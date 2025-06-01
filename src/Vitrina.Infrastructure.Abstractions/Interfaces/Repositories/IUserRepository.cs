using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> SearchUserByEmailAsync(string email);
}
