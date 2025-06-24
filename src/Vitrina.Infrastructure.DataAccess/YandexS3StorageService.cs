using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
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

    public async Task<string> SaveImageAsync(
        Stream fileStream,
        string fileName,
        string path,
        string contentType,
        CancellationToken cancellationToken
    )
    {
        fileName = Guid.NewGuid() + ".webp";

        using var image = await Image.LoadAsync(fileStream, cancellationToken);

        await using var webpStream = new MemoryStream();
        await image.SaveAsWebpAsync(webpStream, cancellationToken);

        webpStream.Seek(0, SeekOrigin.Begin);

        await SaveFileAsync(webpStream, fileName, path, contentType, cancellationToken);
        return fileName;
    }

    public Task<string> GetPreSignedURL(string fileName, string path, TimeSpan validFor)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = path + fileName,
            Expires = DateTime.UtcNow.Add(validFor),
            Verb = HttpVerb.GET
        };

        return s3Client.GetPreSignedURLAsync(request);
    }

    public async Task DeleteFileAsync(string fileName, string path, CancellationToken cancellationToken)
    {
        var removeRequest = new DeleteObjectRequest { BucketName = bucketName, Key = path + fileName };

        await s3Client.DeleteObjectAsync(removeRequest, cancellationToken);
    }

    public async Task SaveFileAsync(
        Stream fileStream,
        string fileName,
        string path,
        string contentType,
        CancellationToken cancellationToken
    )
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = path + fileName,
            InputStream = fileStream,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };

        await s3Client.PutObjectAsync(putRequest, cancellationToken);
    }

    public string GetFileUrl(string fileName)
    {
        return $"https://storage.yandexcloud.net/{bucketName}/{fileName}";
    }
}
