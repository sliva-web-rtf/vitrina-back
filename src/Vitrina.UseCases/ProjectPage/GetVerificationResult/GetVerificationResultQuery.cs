using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetVerificationResult;

public record GetVerificationResultQuery(Guid PageId, int? IdAuthorizedUser) : IRequest<VerificationResultDto>;
