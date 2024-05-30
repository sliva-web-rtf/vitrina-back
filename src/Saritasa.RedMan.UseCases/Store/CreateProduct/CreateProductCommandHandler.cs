using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.DomainServices.Store;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;
using Saritasa.RedMan.UseCases.Store.Common.Exceptions;
using Saritasa.Tools.EntityFrameworkCore;

namespace Saritasa.RedMan.UseCases.Store.CreateProduct;

/// <summary>
/// Handler for <see cref="CreateProductCommand" />.
/// </summary>
internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly ILogger<CreateProductCommandHandler> logger;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="loggedUserAccessor">Logger user accessor.</param>
    public CreateProductCommandHandler(
        IAppDbContext appDbContext,
        IMapper mapper,
        ILogger<CreateProductCommandHandler> logger,
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.logger = logger;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validation.
        if (!new ProductSkuValidator().IsValid(request.Sku))
        {
            throw new InvalidSkuException();
        }

        // Creation.
        var currentUserId = loggedUserAccessor.GetCurrentUserId();

        var product = mapper.Map<Product>(request);
        product.CreatedByUser = await appDbContext.Users.GetAsync(u => u.Id == currentUserId, cancellationToken);
        product.Clean();

        appDbContext.Products.Add(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"The product with id {product.Id} has been created.");

        return product.Id;
    }
}
