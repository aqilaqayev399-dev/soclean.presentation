using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Subscribe : BaseEntity
{
    public string Email { get; set; } = null!;
}