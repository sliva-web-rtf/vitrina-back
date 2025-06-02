using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectThematics.UpdateThematics;

public class UpdateThematicsCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateThematicsCommand, ResponceThematicsDto>
{
    public async Task<ResponceThematicsDto> Handle(UpdateThematicsCommand request, CancellationToken cancellationToken)
    {
        var thematics = await dbContext.ProjectThematics.FindAsync(request.Id, cancellationToken)
                        ?? throw new NotFoundException($"Thematics with id = {request.Id} not found");
        var thematicsDto = mapper.Map<RequestThematicsDto>(thematics);
        request.PatchDocument.ApplyTo(thematicsDto);
        _ = await dbContext.ProjectThematics.FirstOrDefaultAsync(existingThematics =>
                existingThematics.Id != request.Id && existingThematics.Name == thematicsDto.Name, cancellationToken)
            ?? throw new DomainException(
                $"The thematics with {nameof(thematics.Name)} = {thematicsDto.Name} already exists");
        mapper.Map(thematicsDto, thematics);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ResponceThematicsDto>(thematics);
    }
}
