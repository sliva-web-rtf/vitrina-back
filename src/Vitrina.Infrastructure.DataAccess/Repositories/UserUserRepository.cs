using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Common.Repositories;

public class UserUserRepository(IAppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var existingUser = await dbContext.Users.FirstOrDefaultAsync(currentUser => currentUser.Id == user.Id, cancellationToken);
        if (existingUser is null)
        {
            throw new NotFoundException("User not found");
        }

        dbContext.Users.Entry(existingUser).CurrentValues.SetValues(user);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var result = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        if (result == null)
        {
            throw new NotFoundException($"User with email = {email} data was not found");
        }

        return result;
    }
}
