using System.Reflection;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.GetUsers;

namespace Vitrina.UseCases.Tests.User;

[TestFixture]
public class GetUsersTests
{
    [Test]
    public async Task Handle_ValidQuery_ReturnsPagedListOfUsers()
    {
        // Arrange
        var users = new List<Domain.User.User>
        {
            new()
            {
                Id = 1,
                NormalizedEmail = "TEST1@EMAIL.COM",
                FirstName = "Test1",
                LastName = "User1",
                Email = "test1@email.com"
            },
            new()
            {
                Id = 2,
                NormalizedEmail = "TEST2@EMAIL.COM",
                FirstName = "Test2",
                LastName = "User2",
                Email = "test2@email.com"
            },
            new()
            {
                Id = 3,
                NormalizedEmail = "ADMIN@EMAIL.COM",
                FirstName = "Test",
                LastName = "Admin",
                Email = "admin@email.com"
            },
        };

        var fakeDbSet = new FakeDbSet<Domain.User.User>(users);

        var dbContext = A.Fake<IAppDbContext>();
        var mapper = A.Fake<IMapper>();

        A.CallTo(() => dbContext.Users).Returns(fakeDbSet);

        var mappedUsers = new List<ResponceShortenedUserDto>
        {
            new() { FirstName = "Test1", LastName = "User1", Email = "test1@email.com", Avatar = "avatar1.png" },
            new() { FirstName = "Test2", LastName = "User2", Email = "test2@email.com", Avatar = "avatar2.png" },
            new() { FirstName = "Test", LastName = "Admin", Email = "admin@email.com", Avatar = "avatar3.png" },
        };

        A.CallTo(() => mapper.Map<List<ResponceShortenedUserDto>>(A<object>._))
            .Returns(mappedUsers);

        var query = new GetUsersQuery { Page = 1, PageSize = 10 };
        var handler = new GetUsersQueryHandler(dbContext, mapper);

        var method = typeof(GetUsersQueryHandler)
            .GetMethod("ApplyFilters", BindingFlags.NonPublic | BindingFlags.Instance)!;

        var filtered = (IQueryable<Domain.User.User>)method.Invoke(handler, [fakeDbSet, query])!;

        var result = filtered.ToList();
        result.Should().HaveCount(3);
        result.ForEach(user =>
        {
            user.FirstName.Should().NotBeNullOrEmpty();
            user.LastName.Should().NotBeNullOrEmpty();
            user.Email.Should().NotBeNullOrEmpty();
        });
        result[0].NormalizedEmail.Should().Be("TEST1@EMAIL.COM");
        result[1].NormalizedEmail.Should().Be("TEST2@EMAIL.COM");
        result[2].NormalizedEmail.Should().Be("ADMIN@EMAIL.COM");
    }

    [Test]
    public async Task ApplyFilters_WithEmail_FilteredCorrectly()
    {
        var users = new List<Domain.User.User>
        {
            new() { Id = 1, FirstName = null, LastName = null, Email = "user1@mail.com" },
            new() { Id = 2, FirstName = null, LastName = null, Email = "test@mail.com" }
        };

        var mapper = A.Fake<IMapper>();
        A.CallTo(() => mapper.Map<List<ResponceShortenedUserDto>>(A<object>.Ignored))
            .Returns([]);

        var fakeDbSet = new FakeDbSet<Domain.User.User>(users);

        var dbContext = A.Fake<IAppDbContext>();
        A.CallTo(() => dbContext.Users).Returns(fakeDbSet);

        var handler = new GetUsersQueryHandler(dbContext, mapper);
        var query = new GetUsersQuery
        {
            Email = "test@mail.com",
            Page = 1,
            PageSize = 10,
        };

        var method = typeof(GetUsersQueryHandler)
            .GetMethod("ApplyFilters", BindingFlags.NonPublic | BindingFlags.Instance)!;

        var filtered = (IQueryable<Domain.User.User>)method.Invoke(handler, [fakeDbSet, query])!;

        var resultList = filtered.ToList();
        resultList.Should().HaveCount(1);
        resultList[0].NormalizedEmail.Should().Be("TEST@MAIL.COM");
    }
}
