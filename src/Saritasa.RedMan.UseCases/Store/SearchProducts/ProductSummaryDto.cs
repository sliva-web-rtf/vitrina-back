using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.UseCases.Users.Common.Dtos;

namespace Saritasa.RedMan.UseCases.Store.SearchProducts;

/// <summary>
/// Product summary DTO.
/// </summary>
public class ProductSummaryDto
{
    /// <inheritdoc cref="Product.Id"/>
    public int Id { get; set; }

    /// <inheritdoc cref="Product.Name"/>
    required public string Name { get; set; }

    /// <inheritdoc cref="Product.CreatedByUser"/>
    required public UserDto CreatedByUser { get; set; }
}
