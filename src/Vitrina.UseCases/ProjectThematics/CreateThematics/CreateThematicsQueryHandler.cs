using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectThematics.CreateThematics;

public class CreateThematicsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateThematicsCommand, Guid>
{
    public async Task<Guid> Handle(CreateThematicsCommand request, CancellationToken cancellationToken)
    {
        var thematicsDto = request.ThematicsDto;
        _ = await dbContext.ProjectThematics.FirstOrDefaultAsync(existingThematics =>
                existingThematics.Name == thematicsDto.Name, cancellationToken)
            ?? throw new DomainException(
                $"The thematics with {nameof(thematicsDto.Name)} = {thematicsDto.Name} already exists");
        var thematics = mapper.Map<Domain.Project.ProjectThematics>(thematicsDto);
        dbContext.ProjectThematics.Add(thematics);
        await dbContext.SaveChangesAsync(cancellationToken);
        return thematics.Id;
    }
}
