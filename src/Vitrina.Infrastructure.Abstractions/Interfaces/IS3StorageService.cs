namespace Vitrina.Infrastructure.Abstractions.Interfaces;

public interface IS3StorageService
{
    public Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType);

    public Task<string> GetFileURLAsync(string fileName, TimeSpan validFor);

    public string GetFileUrl(string fileName);
}
