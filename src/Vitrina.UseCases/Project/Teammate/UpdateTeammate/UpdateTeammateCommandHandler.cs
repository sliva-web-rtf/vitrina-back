using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Teammate.CreateTeammate;

namespace Vitrina.UseCases.Project.Teammate.UpdateTeammate;

/// <inheritdoc />
public class UpdateTeammateCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateTeammateCommand, TeammateDto>
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
    public async Task<TeammateDto> Handle(UpdateTeammateCommand request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Teammate with the specified id = {request.Id} was not found");
        if (teammate.Project.CheckYourEditingRights(request.IdAuthorizedUser))
        {
            throw new ForbiddenException("You do not have the rights to change the data of this project.");
        }

        if (teammate.User.RegistrationStatus == RegistrationStatusEnum.Registered)
        {
            var paths = request.PatchDocument.Operations.Select(operation => operation.path);
            if (paths.Any(unacceptablePathsEditRegisteredUsers.Contains))
            {
                throw new DomainException(
                    "User registry on the platform, so his personal information cannot be changed.");
            }
        }

        var teammateDto = mapper.Map<TeammateDto>(teammate);
        request.PatchDocument.ApplyTo(teammateDto);
        mapper.Map(teammateDto, teammate);
        await CreateTeammateCommandHandler.AddNewTeammateRolesAsync(dbContext, teammate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return teammateDto;
    }
}
