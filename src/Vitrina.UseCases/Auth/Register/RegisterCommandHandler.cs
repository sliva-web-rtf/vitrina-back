using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Auth.Register;

/// <summary>
///     Handler for <see cref="RegisterCommand" />.
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    private readonly IAppDbContext appDbContext;
    private readonly UserManager<Domain.User.User> userManager;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public RegisterCommandHandler(UserManager<Domain.User.User> userManager, IAppDbContext appDbContext)
    {
        this.userManager = userManager;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc />
    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.User.User.CreteUser(
            request.LastName,
            request.FirstName,
            request.Patronymic,
            request.RoleOnPlatform,
            request.Email,
            request.EducationCourse,
            request.EducationLevel);

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
}
