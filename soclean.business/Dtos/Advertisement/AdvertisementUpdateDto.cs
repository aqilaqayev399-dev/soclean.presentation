using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Advertisement;

public class AdvertisementUpdateDto : IDto
{
    public int Id { get; set; }

    public string? Title { get; set; } 
    public string? Photo { get; set; } 
    public IFormFile? PhotoFile { get; set; } 
}

