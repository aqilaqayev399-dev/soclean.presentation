using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Advertisement : BaseEntity
{
 public string Title { get; set; } = null!;
 public string Photo { get; set; } = null!;
}
