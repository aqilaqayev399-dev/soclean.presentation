using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Advertisement;      

public class AdvertisementDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Photo { get; set; } = null!;
}

