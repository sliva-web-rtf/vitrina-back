using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.User;

/// <summary>
/// Confirmation code for email.
/// </summary>
public class ConfirmationCode
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Confirmation code.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// User id to confirm.
    /// </summary>
    public int UserId { get; set; }
}
