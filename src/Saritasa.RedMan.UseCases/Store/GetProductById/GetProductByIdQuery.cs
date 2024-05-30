using MediatR;

namespace Saritasa.RedMan.UseCases.Store.GetProductById;

/// <summary>
/// Get product by id query.
/// </summary>
public record GetProductByIdQuery : IRequest<ProductDto>
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public int Id { get; init; }
}
