using System.ComponentModel.DataAnnotations;

namespace soclean.business.Dtos.Account;

public class LoginDto
{
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
