using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Teammate.UpdateTeammate;

/// <inheritdoc />
public class UpdateTeammateCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateTeammateCommand, ResponceTeammateDto>
{
    private readonly string[] unacceptablePathsEditRegisteredUsers =
    [
        "/user/firstName",
        "/user/lastName",
        "/user/patronymic",
        "/user/email",
        "user/firstName",
        "user/lastName",
        "user/patronymic",
        "user/email"
    ];

    /// <inheritdoc />
    public async Task<ResponceTeammateDto> Handle(UpdateTeammateCommand request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Teammate with the specified id = {request.Id} was not found");
        teammate.Team.Project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        if (teammate.User.RegistrationStatus == RegistrationStatusEnum.Registered)
        {
            var paths = request.PatchDocument.Operations.Select(operation => operation.path);
            if (paths.Any(path => path == "teamId" || path == "/teamId"))
            {
                throw new DomainException("You can't change the team ID.");
            }

            if (paths.Any(unacceptablePathsEditRegisteredUsers.Contains))
            {
                throw new DomainException(
                    "User registry on the platform, so his personal information cannot be changed.");
            }
        }

        var teammateDto = mapper.Map<ResponceTeammateDto>(teammate);
        request.PatchDocument.ApplyTo(teammateDto);
        mapper.Map(teammateDto, teammate);
        await dbContext.SaveChangesAsync(cancellationToken);
        return teammateDto;
    }
}
