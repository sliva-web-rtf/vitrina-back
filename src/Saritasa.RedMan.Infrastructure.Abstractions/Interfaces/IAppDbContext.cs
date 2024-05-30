using Microsoft.EntityFrameworkCore;
using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.Domain.Users;

namespace Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Products set.
    /// </summary>
    DbSet<Product> Products { get; }
}
