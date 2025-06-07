using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.Auth.GetUserById;

/// <summary>
///     Handler for <see cref="GetUserByIdQuery" />.
/// </summary>
internal class GetUserByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    /// <inheritdoc />
    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users.GetAsync(user => user.Id == request.UserId, cancellationToken);
            return mapper.Map<UserDetailsDto>(user);
        }
        catch (Exception ex)
        {
            throw new DomainException();
        }
    }
}
