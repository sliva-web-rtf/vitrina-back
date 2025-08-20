using FakeItEasy;
using FluentAssertions;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.YandexBucket.Files.GetFilesByCreatorId;
using File = Vitrina.Domain.File;

namespace Vitrina.UseCases.Tests.User;

[TestFixture]
public class GetFilesByCreatorIdTests
{
    private IAppDbContext dbContext;
    private IS3StorageService s3Storage = null!;
    private GetFilesByCreatorIdQueryHandler handler = null!;
    private CancellationToken ct;

    [SetUp]
    public void SetUp()
    {
        dbContext = A.Fake<IAppDbContext>();
        s3Storage = A.Fake<IS3StorageService>();
        handler = new(dbContext, s3Storage);
        ct = CancellationToken.None;
    }

    [Test]
    public async Task Handle_UserExists_ReturnsOnlyFilesByCreator()
    {
        const int userId = 1;

        var user = new Domain.User.User { Id = userId, FirstName = null, LastName = null, Email = null };
        var filesGuids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        var files = new List<File>
        {
            new() { Id = filesGuids[0], CreatorId = 1, Path = "file0.png" },
            new() { Id = filesGuids[1], CreatorId = 2, Path = "file1.png" },
            new() { Id = filesGuids[2], CreatorId = 1, Path = "file2.png" }
        };

        var dbSetFiles = new FakeDbSet<File>(files);

        A.CallTo(() => dbContext.Users.FindAsync(userId, ct))
            .Returns(user);

        A.CallTo(() => dbContext.Files)
            .Returns(dbSetFiles);

        A.CallTo(() => s3Storage.GetPreSignedURL("file0.png", A<TimeSpan>._))
            .Returns("https://url0");

        A.CallTo(() => s3Storage.GetPreSignedURL("file1.png", A<TimeSpan>._))
            .Returns("https://url1");

        A.CallTo(() => s3Storage.GetPreSignedURL("file2.png", A<TimeSpan>._))
            .Returns("https://url2");

        var query = new GetFilesByCreatorIdQuery(userId);

        var result = await handler.Handle(query, ct);

        result.Should().HaveCount(2);
        result.Should().ContainSingle(f => f.Id == filesGuids[0] && f.Url == "https://url0");
        result.Should().ContainSingle(f => f.Id == filesGuids[2] && f.Url == "https://url2");
    }

    [Test]
    public async Task Handle_UserExists_ButNoFiles_ReturnsEmptyList()
    {
        const int userId = 1;

        var user = new Domain.User.User { Id = userId, FirstName = null, LastName = null, Email = null };
        var files = new List<File> { new() { Id = Guid.NewGuid(), CreatorId = 2, Path = "other.png" } };

        A.CallTo(() => dbContext.Users.FindAsync(userId, ct))
            .Returns(user);

        A.CallTo(() => dbContext.Files)
            .Returns(new FakeDbSet<File>(files));

        var query = new GetFilesByCreatorIdQuery(userId);

        var result = await handler.Handle(query, ct);

        result.Should().BeEmpty("пользователь существует, но у него нет файлов");
    }

    [Test]
    public void Handle_UserNotFound_ThrowsNotFoundException()
    {
        const int userId = 1;

        A.CallTo(() => dbContext.Users.FindAsync(userId, ct))
            .Returns(null);

        var query = new GetFilesByCreatorIdQuery(userId);

        var act = async () => await handler.Handle(query, ct);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
        (s3Storage as IDisposable)?.Dispose();
        (handler as IDisposable)?.Dispose();
        ct = CancellationToken.None;
    }
}
