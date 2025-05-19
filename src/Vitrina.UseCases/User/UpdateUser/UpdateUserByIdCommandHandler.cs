using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.UpdateUser;

/// <inheritdoc />
public class UpdateUserByIdCommandHandler(
    UserManager<Domain.User.User> userManager,
    IMapper mapper,
    UpdateUserDtoValidator validator,
    ISpecializationRepository specializationRepository) : IRequestHandler<UpdateUserByIdCommand, object>
{
    private static readonly HashSet<string> CommonPatchPaths =
    [
        "/firstName",
        "/lastName",
        "/patronymic",
        "/telegram",
        "/email",
        "/phoneNumber"
    ];

    private static readonly HashSet<string> StudentPathPaths =
    [
        "/additionalInformation/educationLevel",
        "/additionalInformation/educationCourse",
        "/additionalInformation/resume",
        "/additionalInformation/roleInTeam",
        "/additionalInformation/specialization/name"
    ];

    private static readonly HashSet<string> NotStudentPatchPaths =
    [
        "/additionalInformation/company",
        "/additionalInformation/post"
    ];

    public async Task<object> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync($"{request.Id}") ??
                   throw new NotFoundException($"User with id = {request.Id} not found");

        var invalidPatchPaths = GetInvalidPatchPaths(user.RoleOnPlatform, request.PatchDocument);

        if (invalidPatchPaths.Any())
        {
            throw new DomainException($"The following Patch tracks are not acceptable for this role of the user:" +
                                      $" {string.Join(',', invalidPatchPaths)}");
        }

        var updateUserDto = mapper.Map<UpdateUserDtoBase>(user);
        request.PatchDocument.ApplyTo(updateUserDto);
        var validationResult = await validator.ValidateAsync(updateUserDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException($"Data did not go through the validity check:" +
                                      $"{Environment.NewLine}{string.Join(Environment.NewLine, validationResult.Errors)}");
        }

        var specializationName = updateUserDto.AdditionalInformation.Specialization.Name;
        if (specializationName is not null &&
            await specializationRepository.FindAsync(specializationName, cancellationToken) is null)
        {
            throw new DomainException($"Specialization with Name = {specializationName} was not found.");
        }

        return await TryUpdate(user, updateUserDto);
    }

    private IEnumerable<string> GetInvalidPatchPaths(RoleOnPlatformEnum roleOnPlatformEnum,
        JsonPatchDocument<UpdateUserDtoBase> patchDocument)
    {
        var paths = patchDocument.Operations.Select(operation => operation.path);
        return roleOnPlatformEnum switch
        {
            RoleOnPlatformEnum.Student => paths
                .Where(path => !(CommonPatchPaths.Contains(path) || StudentPathPaths.Contains(path))),
            RoleOnPlatformEnum.Curator or RoleOnPlatformEnum.Partner => paths
                .Where(path => !(CommonPatchPaths.Contains(path) || NotStudentPatchPaths.Contains(path))),
            _ => throw new NotImplementedException(
                $"Logic for {nameof(RoleOnPlatformEnum)} = {roleOnPlatformEnum} is not defined")
        };
    }

    private async Task<object> TryUpdate(Domain.User.User user, UpdateUserDtoBase updateUserDtoBase)
    {
        object result;
        switch (user.RoleOnPlatform)
        {
            case RoleOnPlatformEnum.Student:
                var studentDto = mapper.Map<StudentDtoBase>(updateUserDtoBase);
                mapper.Map(result = studentDto, user);
                break;
            case RoleOnPlatformEnum.Curator or RoleOnPlatformEnum.Partner:
                var notStudent = mapper.Map<NotStudentDtoBase>(updateUserDtoBase);
                user = mapper.Map(result = notStudent, user);
                break;
            default:
                throw new NotImplementedException(
                    $"Logic for {nameof(RoleOnPlatformEnum)} = {user.RoleOnPlatform} is not defined");
        }

        await userManager.UpdateAsync(user);
        return result;
    }
}
