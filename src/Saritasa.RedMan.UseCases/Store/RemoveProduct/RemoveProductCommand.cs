using MediatR;

namespace Saritasa.RedMan.UseCases.Store.RemoveProduct;

/// <summary>
/// Remove product command.
/// </summary>
public record RemoveProductCommand : IRequest
{
    /// <summary>
    /// Product id.
    /// </summary>
    public int Id { get; init; }
}
