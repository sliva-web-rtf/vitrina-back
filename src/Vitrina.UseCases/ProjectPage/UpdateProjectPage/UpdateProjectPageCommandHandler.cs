using AutoMapper;
using FluentValidation;
using MediatR;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.UpdateProjectPage;

/// <inheritdoc />
public class UpdateProjectPageCommandHandler(
    IProjectPageRepository repository,
    IValidator<ContentBlockDto> validator,
    IMapper mapper,
    IAppDbContext dbContext)
    : IRequestHandler<UpdateProjectPageCommand>
{
    /// <inheritdoc />
    public async Task Handle(UpdateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        page.SortContentBlocks();
        var pageDto = mapper.Map<UpdateProjectPageDto>(page);
        request.PatchDocument.ApplyTo(pageDto);

        foreach (var block in pageDto.ContentBlocks)
        {
            var validationResult = await validator.ValidateAsync(block, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        var content = page.ContentBlocks.ToArray();
        page.ContentBlocks.Clear();

        foreach (var blockDto in pageDto.ContentBlocks)
        {
            var block = mapper.Map<ContentBlock>(blockDto);
            page.ContentBlocks.Add(block);

            if (!content.Any(existingBlock => existingBlock.Id == blockDto.Id))
            {
                dbContext.ContentBlocks.Add(block);
            }
        }

        page.NumberCustomBlocks();
        page.ReadyStatus = pageDto.ReadyStatus == PageReadyStatusEnum.Draft
            ? PageReadyStatusEnum.Draft
            : PageReadyStatusEnum.UnderReview;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
