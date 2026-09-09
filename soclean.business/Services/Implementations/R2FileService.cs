using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using soclean.business.Services.Abstract;

namespace soclean.business.Services.Implementations;

public class R2FileService : ICloudManager
{
    private readonly IAmazonS3 _s3;
    private readonly AWSConfigurationDto _cfg;

    public R2FileService(IConfiguration configuration)
    {
        _cfg = configuration.GetSection("AWSSettings").Get<AWSConfigurationDto>() ?? new();

        var s3cfg = new AmazonS3Config
        {
            ServiceURL = $"https://{_cfg.AccountId}.r2.cloudflarestorage.com",
            ForcePathStyle = true // path-style: https://{accountId}.r2.cloudflarestorage.com/{bucket}/{key}
        };
        _s3 = new AmazonS3Client(_cfg.AccessKey, _cfg.SecretKey, s3cfg);
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File boşdur!");

        var key = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
        using var stream = file.OpenReadStream();

        var put = new PutObjectRequest
        {
            BucketName = _cfg.BucketName,
            Key = key,
            InputStream = stream,
            ContentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType,
            UseChunkEncoding = false
        };

        put.Headers.ContentLength = file.Length;

        await _s3.PutObjectAsync(put);

        if (!string.IsNullOrWhiteSpace(_cfg.PublicBaseUrl))
            return $"{_cfg.PublicBaseUrl.TrimEnd('/')}/{key}";

        return $"https://{_cfg.AccountId}.r2.cloudflarestorage.com/{_cfg.BucketName}/{key}";
    }

    public async Task<bool> FileDeleteAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path boşdur!");

        try
        {
            var uri = new Uri(filePath);
            var key = uri.AbsolutePath.TrimStart('/');

            if (uri.Host.Contains(".r2.cloudflarestorage.com", StringComparison.OrdinalIgnoreCase)
                && key.StartsWith(_cfg.BucketName + "/", StringComparison.Ordinal))
            {
                key = key.Substring(_cfg.BucketName.Length + 1);
            }

            var resp = await _s3.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _cfg.BucketName,
                Key = key
            });

            return resp.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }
        catch
        {
            return false;
        }
    }


}
