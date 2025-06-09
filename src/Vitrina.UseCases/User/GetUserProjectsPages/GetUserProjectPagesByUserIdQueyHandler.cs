using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.User.GetUserProjectsPages;

/// <inheritdoc />
public class GetUserProjectPagesByUserIdQueyHandler(UserManager<Domain.User.User> userManager, IMapper mapper)
    : IRequestHandler<GetUserProjectPagesByUserIdQuey, ICollection<ResponceProjectPageDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<ResponceProjectPageDto>> Handle(GetUserProjectPagesByUserIdQuey request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync($"{request.UserId}") ??
                   throw new NotFoundException("The user with the specified Id was not found");

        return mapper.Map<ICollection<ResponceProjectPageDto>>(user.EditingRights.Select(editor => editor.Page));
    }
}
