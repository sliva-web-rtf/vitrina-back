using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUser;

/// <inheritdoc />
public class GetUserByIdQueryHandler(UserManager<Domain.User.User> userManager, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, object>
{
    /// <inheritdoc />
    public async Task<object> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync($"{request.Id}") ??
                   throw new NotFoundException($"User with id = {request.Id} not found");
        return user.RoleOnPlatform switch
        {
            RoleOnPlatformEnum.Student => mapper.Map<StudentDto>(user),
            RoleOnPlatformEnum.Curator or RoleOnPlatformEnum.Partner => mapper.Map<NotStudentDto>(user),
            _ => throw new NotImplementedException(
                $"Logic for {nameof(RoleOnPlatformEnum)} = {user.RoleOnPlatform} is not defined")
        };
    }
}
