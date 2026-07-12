using AutoMapper;
using FluentAssertions;
using Moq;
using TransactionSystem.Application.Common;
using TransactionSystem.Application.Mapping;
using TransactionSystem.Application.Model;
using TransactionSystem.Application.Services;
using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Interfaces;
using Xunit;

namespace TransactionSystem.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserServiceTests()
    {

        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMap>());
        _mapper = config.CreateMapper();
        _userService = new UserService(_repositoryMock.Object, _mapper);

    }

    [Fact]
    public async Task GetByIdAsync_Should_Throw_NotFoundException_When_User_Missing()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(-1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

        var act = async () => await _userService.GetByIdAsync(-1);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task CreateAsync_Should_Generate_Id_And_Persist()
    {

        var ct = TestContext.Current.CancellationToken;

        var dto = new UserManagement { FirstName = "Dawn", LastName= "Brown", Email = "dawn@browne.com" };

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity u, CancellationToken _) => {
                u.Id = 45;
                return u;
            });

        var result = await _userService.CreateAsync(dto, ct);
            
        result.Id.Should().BeGreaterThan(0);
        result.FullName.Should().Be(dto.FirstName+" "+dto.LastName);
        result.Email.Should().Be(dto.Email);
    }
}