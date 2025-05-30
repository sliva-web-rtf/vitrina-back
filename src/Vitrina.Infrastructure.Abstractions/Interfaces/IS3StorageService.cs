namespace Vitrina.Infrastructure.Abstractions.Interfaces;

public interface IS3StorageService
{
    public Task<string> SaveImageAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken
    );

    public Task SaveFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken
    );

    public Task<string> GetPreSignedURL(string fileName, TimeSpan validFor);

    public Task DeleteFileAsync(string fileName, CancellationToken cancellationToken);

    public string GetFileUrl(string fileName);
}
