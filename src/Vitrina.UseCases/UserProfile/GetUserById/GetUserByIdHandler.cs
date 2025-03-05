using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.UserProfile.GetUserById;

public class GetUserByIdHandler(IAppDbContext dbContext) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
    }
}
