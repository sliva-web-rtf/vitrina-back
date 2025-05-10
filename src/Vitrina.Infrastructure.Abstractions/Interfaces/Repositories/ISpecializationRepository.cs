using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

/// <summary>
///     Interface for working with domain objects of specializations
/// </summary>
public interface ISpecializationRepository
{
    /// <summary>
    ///     Getting all kinds of specializations.
    /// </summary>
    public Task<ICollection<Specialization>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    ///     Delete a user by ID.
    /// </summary>
    /// <returns></returns>
    public Task<Specialization> Delete(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Create a user by ID.
    /// </summary>
    public Task<Specialization> Create(string name, CancellationToken cancellationToken);

    /// <summary>
    ///     Find specialization by name.
    /// </summary>
    public Task<Specialization?> FindAsync(string name, CancellationToken cancellationToken);
}
