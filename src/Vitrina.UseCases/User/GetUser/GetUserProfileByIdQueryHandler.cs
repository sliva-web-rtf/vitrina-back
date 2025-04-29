using System.Text.Json;
using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

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

        return JsonDocument.Parse(user.ProfileData);
    }
}
