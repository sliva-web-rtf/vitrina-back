using Bogus;
using Microsoft.EntityFrameworkCore;
using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Domain.Exceptions;

namespace Saritasa.RedMan.Web.Infrastructure.Seeders;

/// <summary>
/// Products seeder.
/// </summary>
internal class ProductsSeeder
{
    private readonly IAppDbContext appDbContext;
    private readonly ILogger<ProductsSeeder> logger;

    private readonly Faker faker = new Faker();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Database context.</param>
    /// <param name="logger">Logger.</param>
    public ProductsSeeder(IAppDbContext appDbContext, ILogger<ProductsSeeder> logger)
    {
        this.appDbContext = appDbContext;
        this.logger = logger;
    }

    /// <summary>
    /// Seed.
    /// </summary>
    /// <param name="numberOfItems">Total items to create.</param>
    /// <param name="userId">User id to assign. If not specified - assign to any user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of created items.</returns>
    public async Task<int> Seed(int numberOfItems, int? userId, CancellationToken cancellationToken = default)
    {
        var count = 0;
        var userIds = userId.HasValue ?
            new[] { userId.Value } :
            await appDbContext.Users.Select(u => u.Id).ToArrayAsync(cancellationToken);

        if (!userIds.Any())
        {
            throw new DomainException("No users to assign product.");
        }

        foreach (var chunk in Saritasa.Tools.Common.Utils.CollectionUtils
            .ChunkSelectRange(Enumerable.Range(0, numberOfItems), chunkSize: 50))
        {
            foreach (var chunkRange in chunk)
            {
                appDbContext.Products.Add(GenerateProduct(userIds));
            }
            count += await appDbContext.SaveChangesAsync(cancellationToken);
        }
        logger.LogInformation($"Created {count} products.");
        return count;
    }

    private Product GenerateProduct(int[] userIds)
        => new Product
        {
            Name = faker.Commerce.ProductName(),
            Sku = "SK" + faker.Random.AlphaNumeric(12).ToUpper(),
            Status = faker.Random.Enum<ProductStatus>(),
            CreatedByUserId = faker.PickRandom(userIds)
        };
}
