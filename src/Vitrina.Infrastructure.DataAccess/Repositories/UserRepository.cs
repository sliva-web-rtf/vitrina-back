using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAppDbContext dbContext;
    private readonly DbSet<User> users;

    public UserRepository(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
        users = this.dbContext.Users;
    }

    public Task<User?> SearchUserByEmailAsync(string email)
    {
        return users.SingleOrDefaultAsync(u => u.Email == email);
    }
}
