using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Vitrina.UseCases.User.Auth.RecoveryPassword;

public class GenerateTokenCommand : IRequest<GenerateTokenCommandResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public IUrlHelper UrlHelper { get; set; }
}
