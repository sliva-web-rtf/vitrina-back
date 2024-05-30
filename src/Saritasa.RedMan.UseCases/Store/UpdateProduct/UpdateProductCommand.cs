using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Saritasa.RedMan.Domain.Store;

namespace Saritasa.RedMan.UseCases.Store.UpdateProduct;

/// <summary>
/// Update product command.
/// </summary>
public record UpdateProductCommand : IRequest<Unit>
{
    /// <inheritdoc cref="Product.Id"/>
    [JsonIgnore]
    public int Id { get; init; }

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
