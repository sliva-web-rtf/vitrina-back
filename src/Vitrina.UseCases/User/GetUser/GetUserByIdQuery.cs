using MediatR;

namespace Vitrina.UseCases.User;

public record GetUserByIdQuery(int Id) : IRequest<object>;
