namespace Vitrina.Domain.Project.Constructor;

public class CommandBlock
{
    //
    // Добавятся целевые поля модели
    //

    /// <summary>
    /// A foreign key for the content that this block belongs to.
    /// </summary>
    public Guid ContentId;

    /// <summary>
    /// The element that the content block belongs to.
    /// </summary>
    public Content Content;
}
