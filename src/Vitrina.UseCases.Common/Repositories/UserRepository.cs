using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Common.Repositories;

public class UserRepository(IAppDbContext dbContext) : IRepository<User>
{
    public async Task<Domain.User.User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Domain.User.User user, CancellationToken cancellationToken)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var existingUser = await dbContext.Users.FindAsync(user.Id, cancellationToken);
        if (existingUser is null)
        {
            throw new NotFoundException("User not found");
        }

        dbContext.Users.Entry(existingUser).CurrentValues.SetValues(user);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
