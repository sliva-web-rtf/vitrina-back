using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO.Profile;
using System.Text.Json;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vitrina.UseCases.User.GetUser;

/// <inheritdoc />
public class GetUserProfileByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserProfileByIdQuery, JsonDocument>
{
    /// <inheritdoc />
    public async Task<JsonDocument> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
                                      throw new NotFoundException("The user with the specified Id was not found");

        switch (user.RoleOnPlatform)
        {
            case RoleOnPlatformEnum.Student:
                UpdateUsersProjectList<StudentDto>(user);
                break;
            case RoleOnPlatformEnum.Curator:
                UpdateUsersProjectList<CuratorDto>(user);
                break;
        }

        return JsonDocument.Parse(user.ProfileData);
    }

    private void UpdateUsersProjectList<TUserDto>(Domain.User.User user) where TUserDto : IHavingProjects
    {
        var userDto = JsonConvert.DeserializeObject<TUserDto>(user.ProfileData);
        userDto.Projects = mapper.Map<ICollection<PreviewProjectDto>>(
            user.PositionsInTeams.Select(t => t.Project));

        user.ProfileData = JsonSerializer.Serialize(userDto);
    }
}
