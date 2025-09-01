using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.VerifyPage;

public record VerifyPageCommand(Guid PageId, VerificationResultDto VerificationDto) : IRequest;
