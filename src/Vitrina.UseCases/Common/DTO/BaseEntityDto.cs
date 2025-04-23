namespace Vitrina.UseCases.ProjectPages.Blocks;

public abstract record BaseEntityDto<TId>
{
    /// <summary>
    ///     ID.
    /// </summary>
    required public TId Id { get; init; }
}
