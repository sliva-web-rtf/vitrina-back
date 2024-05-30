using System.ComponentModel.DataAnnotations;
using MediatR;
using Saritasa.RedMan.Domain.Store;

namespace Saritasa.RedMan.UseCases.Store.CreateProduct;

/// <summary>
/// Create product command.
/// </summary>
public record CreateProductCommand : IRequest<int>
{
    /// <inheritdoc cref="Product.Name"/>
    [Required]
    [MaxLength(256)]
    required public string Name { get; init; }

    /// <inheritdoc cref="Product.Sku"/>
    [Required]
    [MaxLength(100)]
    required public string Sku { get; init; }

    /// <inheritdoc cref="Product.Status"/>
    public ProductStatus Status { get; init; }
}
