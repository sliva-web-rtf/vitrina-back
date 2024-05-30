using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saritasa.RedMan.DomainServices.Store;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;
using Saritasa.RedMan.UseCases.Store.Common.Exceptions;
using Saritasa.Tools.EntityFrameworkCore;

namespace Saritasa.RedMan.UseCases.Store.UpdateProduct;

/// <summary>
/// Handler for <see cref="UpdateProductCommand" />.
/// </summary>
internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly ILogger<UpdateProductCommand> logger;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="loggedUserAccessor">Logger user accessor.</param>
    public UpdateProductCommandHandler(
        IAppDbContext appDbContext,
        IMapper mapper,
        ILogger<UpdateProductCommand> logger,
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.logger = logger;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validation.
        if (!new ProductSkuValidator().IsValid(request.Sku))
        {
            throw new InvalidSkuException();
        }

        var product = await appDbContext.Products.Include(p => p.CreatedByUser)
            .GetAsync(p => p.Id == request.Id, cancellationToken);

        mapper.Map(request, product);
        product.UpdatedByUserId = loggedUserAccessor.GetCurrentUserId();
        product.UpdatedAt = DateTime.UtcNow;
        product.Clean();

        await appDbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"The product with id {product.Id} has been updated.");

        return default;
    }
}
