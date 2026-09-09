using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Advertisement;

public class AdvertisementCreateDto : IDto
{
    public string Title { get; set; } = null!;
    public IFormFile PhotoFile { get; set; } = null!;
}

