using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.GetOrganizations;

namespace Vitrina.UseCases.Tests.Project;

public class GetOrganizationsTests
{
    private IAppDbContext dbContext = null!;
    private GetOrganizationsQueryHandler handler = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;

        var context = new AppDbContext(options);
        dbContext = context;
        handler = new GetOrganizationsQueryHandler(dbContext);
    }

    [Test]
    public async Task WhenProjectsHaveClients_ReturnsDistinctClients()
    {
        dbContext.Projects.AddRange(
            new Domain.Project.Project
            {
                Client = "Org1", Name = "Prog1", PageId = default, Page = null, CreatorId = 0
            },
            new Domain.Project.Project
            {
                Client = "Org2", Name = "Prog2", PageId = default, Page = null, CreatorId = 0
            },
            new Domain.Project.Project
            {
                Client = "Org1", Name = "Prog3", PageId = default, Page = null, CreatorId = 0
            });
        await dbContext.SaveChangesAsync();

        var result = await handler.Handle(new GetOrganizationsQuery(), CancellationToken.None);

        result.Should().BeEquivalentTo(["Org1", "Org2"]);
    }

    [Test]
    public async Task WhenNoClients_ReturnsEmptyList()
    {
        dbContext.Projects.Add(new() { Client = null, Name = "Prog1", PageId = default, Page = null, CreatorId = 0 });
        await dbContext.SaveChangesAsync();

        var result = await handler.Handle(new GetOrganizationsQuery(), CancellationToken.None);

        result.Should().BeEmpty();
    }
}
