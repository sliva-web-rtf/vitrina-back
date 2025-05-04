using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO.Profile.Base;

namespace Vitrina.UseCases.User;

public class GetUserByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, JsonElement>
{
    public Task<JsonElement> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(request.RoleOnPlatform switch
        {
            RoleOnPlatformEnum.Curator => GetSerializeUser<Curator, NotStudentDto>(dbContext.Curators, request.Id),
            RoleOnPlatformEnum.Student => GetSerializeUser<Student, NotStudentDto>(dbContext.Students, request.Id),
            RoleOnPlatformEnum.Partner => GetSerializeUser<Partner, NotStudentDto>(dbContext.Partners, request.Id),
            _ => throw new InvalidOperationException("The user was not found")
        });

    private JsonElement GetSerializeUser<TUser, TUserDto>(DbSet<TUser> dbSet, int id) where TUser : UserWithRoleBase
    {
        var user = GetUser(dbSet, id);
        var dto = mapper.Map<TUser, TUserDto>(user);
        return JsonDocument.Parse(JsonSerializer.Serialize(dto)).RootElement;
    }

    public TUser GetUser<TUser>(DbSet<TUser> dbSet, int id) where TUser : UserWithRoleBase
    {
        var user = dbSet.FirstOrDefault(curator => curator.UserId == id);
        if (user is null)
        {
            throw new NotFoundException("Failed to find a user with this role on the platform");
        }

        return user;
    }
}
