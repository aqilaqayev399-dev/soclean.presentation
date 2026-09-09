using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Subscribe;

public class SubscribeDto : IDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
}
