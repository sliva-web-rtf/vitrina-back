using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.UseCases.User;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.UpdateUser;
using Vitrina.UseCases.UserSpecialization;
using Vitrina.UseCases.User.DTO.AdditionalInformation;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Tests.User;

[TestFixture]
public class UpdateUserByIdTests
{
    private UserManager<Domain.User.User> userManager = null!;
    private IMapper mapper = null!;
    private FakeUpdateUserDtoValidator validator = null!;
    private ISpecializationRepository specializationRepo = null!;
    private UpdateUserByIdCommandHandler handler = null!;
    private readonly CancellationToken ct = CancellationToken.None;

    [SetUp]
    public void SetUp()
    {
        var userStore = A.Fake<IUserStore<Domain.User.User>>();
        userManager = A.Fake<UserManager<Domain.User.User>>(x => x.WithArgumentsForConstructor(() =>
            new UserManager<Domain.User.User>(userStore, null, null, null, null, null, null, null, null)));

        mapper = A.Fake<IMapper>();
        validator = A.Fake<FakeUpdateUserDtoValidator>();
        specializationRepo = A.Fake<ISpecializationRepository>();

        handler = new UpdateUserByIdCommandHandler(userManager, mapper, validator, specializationRepo);
    }

    [Test]
    public void InvalidPatchPath_ThrowsDomainException()
    {
        var user = new Domain.User.User
        {
            Id = 1, RoleOnPlatform = RoleOnPlatformEnum.Student, FirstName = null, LastName = null, Email = null
        };
        var patch = new JsonPatchDocument<UserDto>();
        patch.Replace(x => x.FirstName, "value");

        A.CallTo(() => userManager.FindByIdAsync("1")).Returns(user);

        var command = new UpdateUserByIdCommand(1, patch);
        var act = async () => await handler.Handle(command, ct);

        act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public void UserNotFound_ThrowsNotFoundException()
    {
        A.CallTo(() => userManager.FindByIdAsync("1")).Returns(Task.FromResult<Domain.User.User?>(null));

        var command = new UpdateUserByIdCommand(1, new JsonPatchDocument<UserDto>());
        var act = async () => await handler.Handle(command, ct);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void ValidationFails_ThrowsDomainException()
    {
        var user = new Domain.User.User
        {
            Id = 1, RoleOnPlatform = RoleOnPlatformEnum.Student, FirstName = null, LastName = null, Email = null
        };
        var patch = new JsonPatchDocument<UserDto>();
        var dto = new UserDto
        {
            AdditionalInformation = new AdditionalUserInfo
            {
                Specialization = new SpecializationDto { Name = "Curator", Id = default }
            }
        };

        A.CallTo(() => userManager.FindByIdAsync("1")).Returns(user);
        A.CallTo(() => mapper.Map<UserDto>(user)).Returns(dto);
        A.CallTo(() => validator.ValidateAsync(dto, ct))
            .Returns(new ValidationResult(new[] { new ValidationFailure("field", "error") }));

        var command = new UpdateUserByIdCommand(1, patch);
        var act = async () => await handler.Handle(command, ct);

        act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public void SpecializationNotFound_ThrowsDomainException()
    {
        var user = new Domain.User.User
        {
            Id = 1, RoleOnPlatform = RoleOnPlatformEnum.Student, FirstName = null, LastName = null, Email = null
        };
        var patch = new JsonPatchDocument<UserDto>();
        var dto = new UserDto
        {
            AdditionalInformation = new AdditionalUserInfo
            {
                Specialization = new SpecializationDto
                {
                    Name = "MissingSpec",
                    Id = default
                }
            }
        };

        A.CallTo(() => userManager.FindByIdAsync("1")).Returns(user);
        A.CallTo(() => mapper.Map<UserDto>(user)).Returns(dto);
        A.CallTo(() => validator.ValidateAsync(dto, ct))
            .Returns(new ValidationResult());

        var command = new UpdateUserByIdCommand(1, patch);
        var act = async () => await handler.Handle(command, ct);

        act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public void InvalidRole_ThrowsNotImplementedException()
    {
        var user = new Domain.User.User
        {
            Id = 1, RoleOnPlatform = (RoleOnPlatformEnum)999, FirstName = null, LastName = null, Email = null
        };
        var patch = new JsonPatchDocument<UserDto>();

        A.CallTo(() => userManager.FindByIdAsync("1")).Returns(user);

        var command = new UpdateUserByIdCommand(1, patch);
        var act = async () => await handler.Handle(command, ct);

        act.Should().ThrowAsync<NotImplementedException>();
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

public class FakeUpdateUserDtoValidator(ValidationResult result) : UpdateUserDtoValidator
{
    public virtual Task<ValidationResult> ValidateAsync(UserDto instance, CancellationToken cancellation = default)
    {
        return Task.FromResult(result);
    }
}

