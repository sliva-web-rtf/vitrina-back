using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.User.Auth.ForgotPassword;

public class ForgotPasswordCommand : IRequest<ForgotPasswordCommandResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public IUrlHelper UrlHelper { get; set; }
}
