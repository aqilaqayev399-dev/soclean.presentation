namespace soclean.business.Services.Implementations;

public class AWSConfigurationDto
{
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string AccountId { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string PublicBaseUrl { get; set; } = null!;
}
