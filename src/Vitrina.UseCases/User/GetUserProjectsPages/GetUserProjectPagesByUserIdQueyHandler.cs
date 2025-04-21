using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPages;

namespace Vitrina.UseCases.User.GetUserProjectsPages;

public class GetUserProjectPagesByUserIdQueyHandler(IUserRepository repository, IMapper mapper)
    : IRequestHandler<GetUserProjectPagesByUserIdQuey, ICollection<ProjectPageDto>>
{
    public async Task<ICollection<ProjectPageDto>> Handle(GetUserProjectPagesByUserIdQuey request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken) ??
                   throw new NotFoundException("The user with the specified Id was not found");

        return mapper.Map<ICollection<ProjectPageDto>>(user.ProjectPages);
    }
}
