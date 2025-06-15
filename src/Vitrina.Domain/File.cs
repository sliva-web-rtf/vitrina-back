using System.ComponentModel.DataAnnotations;
using Saritasa.Tools.Domain.Exceptions;

namespace Vitrina.Domain;

public class File
{
    [Key] required public Guid Id { get; init; }

    required public string Path { get; set; }

    required public int CreatorId { get; init; }

    public User.User Creator { get; init; }

    public void ThrowExceptionIfNoAccess(int userId)
    {
        if (userId == CreatorId)
        {
            throw new ForbiddenException("Действия, изменяющие файл для вас запрещены.");
        }
    }
}
