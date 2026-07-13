using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Enums;
using Xunit;

namespace TransactionSystem.IntegrationTests;

public class TransactionsControllerTests : IClassFixture<CustomWebApplicationFactory>
{

    private readonly HttpClient _client;

    public TransactionsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Then_Get_Transactions_Should_Return_Created_Transaction()
    {

        var ct = TestContext.Current.CancellationToken;
        var user = await CreateUserAsync();

        var createDto = new TransactionManagement
        {
            UserId = user.Id,
            Amount = 123.45m,
            TransactionType = TransactionType.Credit
        };

        var postResponse = await _client.PostAsJsonAsync("/api/transactions", createDto, ct);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var getResponse = await _client.GetAsync("/api/transactions", ct);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var transactions = await getResponse.Content.ReadFromJsonAsync<List<TransactionList>>(ct);
        transactions.Should().ContainSingle(t => t.UserId == user.Id && t.Amount == 123.45m);
    }

    [Fact]
    public async Task HighVolume_Endpoint_Should_Return_Transactions_Ordered_Descending()
    {

        var ct = TestContext.Current.CancellationToken;
        var user = await CreateUserAsync();

        await _client.PostAsJsonAsync("/api/transactions", new TransactionManagement
        { UserId = user.Id, Amount = 50m, TransactionType = TransactionType.Debit }, ct);

        await _client.PostAsJsonAsync("/api/transactions", new TransactionManagement
        { UserId = user.Id, Amount = 5000m, TransactionType = TransactionType.Credit }, ct);

        await _client.PostAsJsonAsync("/api/transactions", new TransactionManagement
        { UserId = user.Id, Amount = 2500m, TransactionType = TransactionType.Credit }, ct);

        var response = await _client.GetAsync("/api/transactions/high-volume?threshold=1000", ct);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<TransactionList>>(ct);

        result.Should().HaveCount(2);
        result!.Should().BeInDescendingOrder(t => t.Amount);
        result[0].Amount.Should().Be(5000m);
    }

    [Fact]
    public async Task SummaryByUser_Endpoint_Should_Return_Correct_Totals()
    {

        var ct = TestContext.Current.CancellationToken;
        var user = await CreateUserAsync();

        await _client.PostAsJsonAsync("/api/transactions", new TransactionManagement
        { UserId = user.Id, Amount = 100m, TransactionType = TransactionType.Credit }, ct);
        await _client.PostAsJsonAsync("/api/transactions", new TransactionManagement
        { UserId = user.Id, Amount = 25m, TransactionType = TransactionType.Debit }, ct);

        var response = await _client.GetAsync("/api/transactions/summary/by-user", ct);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<TransactionSummary<int>>>(ct);

        result.Should().Contain(r => r.Key == user.Id && r.TotalAmount == 125m);
    }

    private async Task<User> CreateUserAsync()
    {
        var createUserDto = new UserManagement { FirstName = "George", LastName = "Orwell", Email = $"{Guid.NewGuid()}@example.com" };
        var response = await _client.PostAsJsonAsync("/api/users", createUserDto);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var user = await response.Content.ReadFromJsonAsync<User>();
        return user!;
    }
}