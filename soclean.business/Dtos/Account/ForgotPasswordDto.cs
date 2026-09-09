using System.ComponentModel.DataAnnotations;

namespace soclean.business.Dtos.Account;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
