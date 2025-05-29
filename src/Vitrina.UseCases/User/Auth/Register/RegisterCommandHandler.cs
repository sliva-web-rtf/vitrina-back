using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.Auth.Register;

/// <summary>
///     Handler for <see cref="RegisterCommand" />.
/// </summary>
public class RegisterCommandHandler(
    UserManager<Domain.User.User> userManager,
    IAppDbContext appDbContext,
    IMapper mapper,
    UpdateUserDtoValidator validator)
    : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    /// <inheritdoc />
    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await CreateUserAsync(request, cancellationToken);
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return new RegisterCommandResult
            {
                IsSuccess = false, Message = string.Join("\n", result.Errors.Select(e => e.Description))
            };
        }

        await userManager.AddToRoleAsync(user, $"{request.RoleOnPlatform}");

        var code = ConfirmationCodeGenerator.Generate();
        try
        {
            var confirmationCode = new ConfirmationCode { UserId = user.Id, Code = code };
            await appDbContext.Codes.AddAsync(confirmationCode, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RegisterCommandResult { IsSuccess = false, Message = "failed to save confirmation code." };
        }

        return new RegisterCommandResult { IsSuccess = true, UserId = user.Id, ConfirmationCode = code };
    }

    private async Task<Domain.User.User> CreateUserAsync(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userDto = mapper.Map<UpdateUserDtoBase>(request);
        userDto.AdditionalInformation.EducationCourse = request.EducationCourse;
        var validationResult = await validator.ValidateAsync(userDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException($"Data did not go through the validity check:" +
                                      $"{Environment.NewLine}{string.Join(Environment.NewLine, validationResult.Errors)}");
        }

        var user = userDto.RoleOnPlatform switch
        {
            RoleOnPlatformEnum.Student => mapper.Map<Domain.User.User>(mapper.Map<StudentDtoBase>(userDto)),
            RoleOnPlatformEnum.Curator or RoleOnPlatformEnum.Partner => mapper.Map<Domain.User.User>(
                mapper.Map<NotStudentDtoBase>(userDto)),
            _ => throw new NotImplementedException(
                $"Logic for {nameof(RoleOnPlatformEnum)} = {userDto.RoleOnPlatform} is not defined")
        };
        user.UserName = $"{Guid.NewGuid()}";
        user.RegistrationStatus = RegistrationStatusEnum.Registered;
        return user;
    }
}
