using System.Text.Json;
using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUserProjects;

public class GetUserProjectsByUserIdQueryHandler(IUserRepository repository, IMapper mapper)
    : IRequestHandler<GetUserProjectsByUserIdQuery, ICollection<PreviewProjectDto>>
{
    public async Task<ICollection<PreviewProjectDto>> Handle(GetUserProjectsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken) ??
                   throw new NotFoundException("The user with the specified Id was not found");

        return mapper.Map<ICollection<PreviewProjectDto>>(
            user.PositionsInTeams
                .Select(t => t.Project));
    }
}
