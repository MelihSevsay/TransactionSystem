using AutoMapper;
using FluentAssertions;
using Moq;
using TransactionSystem.Application.Mapping;
using TransactionSystem.Application.Model;
using TransactionSystem.Application.Services;
using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Enums;
using TransactionSystem.Domain.Interfaces;
using Xunit;


namespace TransactionSystem.UnitTests.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _repositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly IMapper _mapper;
    private readonly TransactionService _transactionService;

    public TransactionServiceTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TransactionMap>());
        _mapper = config.CreateMapper();
        _transactionService = new TransactionService(_repositoryMock.Object, _mapper, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetTotalAmountPerUserAsync_Should_Aggregate_Correctly()
    {

        var ct = TestContext.Current.CancellationToken;

        var transactions = new List<TransactionEntity>
        {
            new() { Id = Guid.NewGuid(), UserId = 1, Amount = 100m, TransactionType = TransactionType.Credit },
            new() { Id = Guid.NewGuid(), UserId = 1, Amount = 30m, TransactionType = TransactionType.Debit },
            new() { Id = Guid.NewGuid(), UserId = 2, Amount = 70m, TransactionType = TransactionType.Credit },
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(transactions);

        var result = await _transactionService.GetTotalAmountPerUserAsync(ct);

        result.Should().HaveCount(2);
        result.Single(r => r.Key == 1).TotalAmount.Should().Be(130m);
        result.Single(r => r.Key == 2).TotalAmount.Should().Be(70m);
    }

    [Fact]
    public async Task AddAsync_Should_Map_Dto_To_Entity_And_Persist()
    {

        var ct = TestContext.Current.CancellationToken;

        var dto = new TransactionManagement
        {
            UserId = 1,
            Amount = 42m,
            TransactionType = TransactionType.Debit
        };

        _userRepositoryMock
            .Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<TransactionEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TransactionEntity t, CancellationToken _) => t);

        var result = await _transactionService.AddAsync(dto, ct);

        result.UserId.Should().Be(1);
        result.Amount.Should().Be(42m);
        result.TransactionType.Should().Be(TransactionType.Debit);
    }
}
