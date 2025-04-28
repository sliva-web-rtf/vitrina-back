using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Common.Repositories;

public class SpecializationRepository(IAppDbContext dbContext) : ISpecializationRepository
{
    /// <inheritdoc />
    public async Task<ICollection<Specialization>> GetAll(CancellationToken cancellationToken) =>
        await dbContext.Specializations.ToArrayAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Specialization> Delete(int id, CancellationToken cancellationToken)
    {
        var specialization = await dbContext.Specializations.FindAsync(id, cancellationToken);

        if (specialization is null)
        {
            throw new NotFoundException($"No specialization with ID equal to {id} was found");
        }

        dbContext.Specializations.Remove(specialization);
        await dbContext.SaveChangesAsync(cancellationToken);
        return specialization;
    }

    /// <inheritdoc />
    public async Task<Specialization> Create(string name, CancellationToken cancellationToken)
    {
        var specialization = new Specialization { Name = name };
        dbContext.Specializations.Add(specialization);
        await dbContext.SaveChangesAsync(cancellationToken);
        return specialization;
    }
}
