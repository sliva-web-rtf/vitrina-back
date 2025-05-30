using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.Infrastructure.DataAccess;

public class YandexS3StorageService : IS3StorageService
{
    private readonly AmazonS3Client s3Client;
    private readonly string bucketName;

    public YandexS3StorageService(IConfiguration config)
    {
        bucketName = config["YandexS3:BucketName"];
        var amazonConfig = new AmazonS3Config { ServiceURL = "https://s3.yandexcloud.net", ForcePathStyle = true };

        s3Client = new AmazonS3Client(
            new BasicAWSCredentials(config["YandexS3:AccessKey"], config["YandexS3:SecretKey"]),
            amazonConfig);
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };

        await s3Client.PutObjectAsync(putRequest, cancellationToken);
        return GetFileUrl(fileName);
    }

    public Task<string> GetPreSignedURL(string fileName, TimeSpan validFor)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName, Key = fileName, Expires = DateTime.UtcNow.Add(validFor), Verb = HttpVerb.GET
        };

        return s3Client.GetPreSignedURLAsync(request);
    }

    public async Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var removeRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = fileName
        };

        var a = await s3Client.DeleteObjectAsync(removeRequest, cancellationToken);
        return true;
    }

    public string GetFileUrl(string fileName)
    {
        return $"https://storage.yandexcloud.net/{bucketName}/{fileName}";
    }
}
