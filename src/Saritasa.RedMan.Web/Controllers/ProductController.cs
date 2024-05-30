using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saritasa.RedMan.UseCases.Store.CreateProduct;
using Saritasa.RedMan.UseCases.Store.GetProductById;
using Saritasa.RedMan.UseCases.Store.RemoveProduct;
using Saritasa.RedMan.UseCases.Store.SearchProducts;
using Saritasa.RedMan.UseCases.Store.UpdateProduct;
using Saritasa.Tools.Common.Pagination;

namespace Saritasa.RedMan.Web.Controllers;

/// <summary>
/// Products controller.
/// </summary>
[ApiController]
[Route("api/products")]
[ApiExplorerSettings(GroupName = "store")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Create product.
    /// </summary>
    /// <param name="command">Command.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Id of the created product.</returns>
    [HttpPost]
    public Task<int> CreateProduct(CreateProductCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Update product.
    /// </summary>
    /// <param name="id">Product id.</param>
    /// <param name="command">Command.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    [HttpPut("{id}")]
    public async Task UpdateProduct(int id, UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var updateCommand = command with
        {
            Id = id
        };
        await mediator.Send(updateCommand, cancellationToken);
    }

    /// <summary>
    /// Search products.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Products DTOs.</returns>
    [HttpGet]
    public async Task<PagedListMetadataDto<ProductSummaryDto>> SearchProducts([FromQuery] SearchProductsQuery query, CancellationToken cancellationToken)
        => (await mediator.Send(query, cancellationToken)).ToMetadataObject();

    /// <summary>
    /// Get product.
    /// </summary>
    /// <param name="id">Product id.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Products DTO.</returns>
    [HttpGet("{id}")]
    public Task<ProductDto> GetProduct(int id, CancellationToken cancellationToken)
        => mediator.Send(new GetProductByIdQuery
        {
            Id = id
        }, cancellationToken);

    /// <summary>
    /// Remove product by id.
    /// </summary>
    /// <param name="id">Product id.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    [HttpDelete("{id}")]
    public Task RemoveProduct(int id, CancellationToken cancellationToken)
        => mediator.Send(new RemoveProductCommand
        {
            Id = id
        }, cancellationToken);
}
