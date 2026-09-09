using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Contact : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsAnswer { get; set; }
}
