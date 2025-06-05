using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.UpdateUser;

public record UpdateUserByIdCommand(int Id, JsonPatchDocument<UserDto> PatchDocument) : IRequest<object>;
