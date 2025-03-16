using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.UserProfile.GetUserById;

/// <inheritdoc />
public class GetUserByIdHandler<TResultDto>(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery<TResultDto>, TResultDto>
{
    /// <inheritdoc />
    public async Task<TResultDto> Handle(GetUserByIdQuery<TResultDto> request, CancellationToken cancellationToken)
    {
        return mapper.Map<TResultDto>(await userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
                                      throw new NotFoundException("The user with the specified Id was not found"));
    }
}
