using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.GetUser;
using Microsoft.AspNetCore.Identity;

namespace Vitrina.UseCases.Tests.User
{
    [TestFixture]
    public class GetUserByIdQueryHandlerTests
    {
        private UserManager<Domain.User.User> userManager = null!;
        private IMapper mapper = null!;
        private GetUserByIdQueryHandler handler = null!;
        private CancellationToken cancellationToken;

        [SetUp]
        public void SetUp()
        {
            var userStore = A.Fake<IUserStore<Domain.User.User>>();

            userManager = A.Fake<UserManager<Domain.User.User>>(options => options.WithArgumentsForConstructor(() =>
                new UserManager<Domain.User.User>(userStore,
                    null, null, null, null, null, null, null, null)));

            mapper = A.Fake<IMapper>();
            handler = new GetUserByIdQueryHandler(userManager, mapper);
            cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task StudentFound_ReturnsMappedDto()
        {
            var student = new Domain.User.User
            {
                Id = 1,
                Email = "test@example.com",
                FirstName = null,
                LastName = null,
                RoleOnPlatform = RoleOnPlatformEnum.Student
            };
            var dto = new StudentDto { Email = "test@example.com" };

            A.CallTo(() => userManager.FindByIdAsync("1"))!.Returns(Task.FromResult(student));
            A.CallTo(() => mapper.Map<StudentDto>(student)).Returns(dto);

            var query = new GetUserByIdQuery(1);

            var result = await handler.Handle(query, cancellationToken);

            result.Should().BeEquivalentTo(dto);
        }

        [Test]
        public async Task NotStudentFound_ReturnsMappedDto()
        {
            var notStudent = new Domain.User.User
            {
                Id = 1,
                Email = "test@example.com",
                FirstName = null,
                LastName = null,
                RoleOnPlatform = RoleOnPlatformEnum.Curator
            };
            var dto = new NotStudentDto { Email = "test@example.com" };

            A.CallTo(() => userManager.FindByIdAsync("1"))!.Returns(Task.FromResult(notStudent));
            A.CallTo(() => mapper.Map<NotStudentDto>(notStudent)).Returns(dto);

            var query = new GetUserByIdQuery(1);

            var result = await handler.Handle(query, cancellationToken);

            result.Should().BeEquivalentTo(dto);
        }

        [Test]
        public void UserRolesNotFound_ThrowsException()
        {
            var notStudent = new Domain.User.User
            {
                Id = 1, Email = "test@example.com", FirstName = null, LastName = null
            };

            A.CallTo(() => userManager.FindByIdAsync("1"))!.Returns(Task.FromResult(notStudent));

            var query = new GetUserByIdQuery(1);

            var act = async () => await handler.Handle(query, cancellationToken);

            act.Should().ThrowAsync<NotImplementedException>();
        }

        [Test]
        public void UserNotFound_ThrowsException()
        {
            A.CallTo(() => userManager.FindByIdAsync("999")).Returns(Task.FromResult<Domain.User.User?>(null));

            var query = new GetUserByIdQuery(999);
            var act = async () => await handler.Handle(query, cancellationToken);

            act.Should().ThrowAsync<InvalidOperationException>();
        }

        [TearDown]
        public void TearDown()
        {
            if (userManager is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
