using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.UserProfile.GetUserById;

/// <inheritdoc />
public class GetUserByIdHandler<TDto>(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery<TDto>, TDto>
{
    /// <inheritdoc />
    public async Task<TDto> Handle(GetUserByIdQuery<TDto> request, CancellationToken cancellationToken)
    {
        return mapper.Map<TDto>(await userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
                             throw new NotFoundException("The user with the specified Id was not found"));
    }
}
