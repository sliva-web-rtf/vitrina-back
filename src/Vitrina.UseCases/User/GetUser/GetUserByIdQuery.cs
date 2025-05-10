using MediatR;

namespace Vitrina.UseCases.User.GetUser;

public record GetUserByIdQuery(int Id) : IRequest<object>;
