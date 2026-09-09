using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Subscribe;

public class SubscribeCreateDto : IDto
{
    public string Email { get; set; } = null!;
}
