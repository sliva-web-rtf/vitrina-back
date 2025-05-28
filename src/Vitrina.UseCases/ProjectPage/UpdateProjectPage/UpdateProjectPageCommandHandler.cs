using AutoMapper;
using FluentValidation;
using MediatR;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPages;

namespace Vitrina.UseCases.ProjectPage.UpdateProjectPage;

/// <inheritdoc />
public class UpdateProjectPageCommandHandler(
    IProjectPageRepository repository,
    IValidator<ContentBlockDto> validator,
    IMapper mapper)
    : IRequestHandler<UpdateProjectPageCommand>
{
    /// <inheritdoc />
    public async Task Handle(UpdateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        page.SortContentBlocks();
        var pageDto = mapper.Map<ProjectPageDto>(page);
        request.PatchDocument.ApplyTo(pageDto);
        if (pageDto.Id != request.Id)
        {
            throw new ArgumentException("Invalid page id.");
        }

        foreach (var block in pageDto.ContentBlocks)
        {
            var validationResult = await validator.ValidateAsync(block, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        mapper.Map(pageDto, page);
        page.NumberCustomBlocks();
        page.ReadyStatus = pageDto.ReadyStatus == PageReadyStatusEnum.Draft
            ? PageReadyStatusEnum.Draft
            : PageReadyStatusEnum.UnderReview;
        await repository.Update(page, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
