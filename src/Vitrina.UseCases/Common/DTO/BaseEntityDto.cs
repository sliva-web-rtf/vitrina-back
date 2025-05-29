namespace Vitrina.UseCases.Common.DTO;

public abstract record BaseEntityDto<TId>
{
    /// <summary>
    ///     ID.
    /// </summary>
    required public TId Id { get; init; }
}
