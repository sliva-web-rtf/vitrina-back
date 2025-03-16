using AutoMapper;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.UserProfile.GetUserById;

public class GetStudentByIdHandler(IUserRepository userRepository, IMapper mapper);
