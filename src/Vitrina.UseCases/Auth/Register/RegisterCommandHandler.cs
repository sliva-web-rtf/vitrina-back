using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.Auth.Register;

/// <summary>
///     Handler for <see cref="RegisterCommand" />.
/// </summary>
public class RegisterCommandHandler(
    UserManager<Domain.User.User> userManager,
    IAppDbContext appDbContext,
    IMapper mapper)
    : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    /// <inheritdoc />
    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = CreateUser(request);
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

    private Domain.User.User CreateUser(RegisterCommand request)
    {
        var user = request.RoleOnPlatform switch
        {
            RoleOnPlatformEnum.Student => mapper.Map<Domain.User.User>(mapper.Map<StudentDto>(request)),
            RoleOnPlatformEnum.Curator or RoleOnPlatformEnum.Partner => mapper.Map<Domain.User.User>(
                mapper.Map<NotStudentDto>(request)),
            _ => throw new NotImplementedException(
                $"Logic for {nameof(RoleOnPlatformEnum)} = {request.RoleOnPlatform} is not defined")
        };
        user.UserName = $"{Guid.NewGuid()}";
        user.RegistrationStatus = RegistrationStatusEnum.Registered;
        return user;
    }
}
