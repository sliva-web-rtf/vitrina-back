using MediatR;
using Saritasa.RedMan.UseCases.Common.Pagination;
using Saritasa.Tools.Common.Pagination;

namespace Saritasa.RedMan.UseCases.Store.SearchProducts;

/// <summary>
/// Search products query.
/// </summary>
public class SearchProductsQuery : PageQueryFilter, IRequest<PagedList<ProductSummaryDto>>
{
    /// <summary>
    /// Name search term.
    /// </summary>
    public string? NameTerm { get; init; }
}
