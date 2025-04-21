using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using System.Text.Json;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.User.GetUser;

/// <inheritdoc />
public class GetUserProfileByIdQueryHandler(IRepository<Domain.User.User> repository, IMapper mapper)
    : IRequestHandler<GetUserProfileByIdQuery, JsonDocument>
{
    /// <inheritdoc />
    public async Task<JsonDocument> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken) ??
                                      throw new NotFoundException("The user with the specified Id was not found");

        return JsonDocument.Parse(user.ProfileData);
    }
}
