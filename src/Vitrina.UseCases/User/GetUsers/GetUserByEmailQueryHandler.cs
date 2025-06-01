using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <inheritdoc />
public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ResponceShortenedUserDto>
{
    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    private readonly IUserRepository userRepository;

    /// <inheritdoc />
    public async Task<ResponceShortenedUserDto> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.SearchUserByEmailAsync(request.Email) ??
                   throw new NotFoundException($"User with email {request.Email} not found.");

        return new ResponceShortenedUserDto(user.Avatar);
    }
}
