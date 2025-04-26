using AutoMapper;
using FluentValidation;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.ProjectPages.UpdateProjectPage;

namespace Vitrina.UseCases.ProjectPage.UpdateProjectPage;

public class UpdateProjectPageCommandHandler(IProjectPageRepository repository, IValidator<ContentBlockDto> validator, IMapper mapper)
    : IRequestHandler<UpdateProjectPageCommand>
{
    public async Task Handle(UpdateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
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
        await repository.Update(page, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
