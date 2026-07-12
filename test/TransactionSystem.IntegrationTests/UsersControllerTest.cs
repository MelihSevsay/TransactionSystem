using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TransactionSystem.Application.Model;
using Xunit;

namespace TransactionSystem.IntegrationTests
{
    public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UsersControllerTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task Create_Then_GetById_Should_Return_The_Same_User()
        {


            var ct = TestContext.Current.CancellationToken;

            var createUser = new UserManagement { FirstName = "William", LastName = "Shekspeare", Email = "willia.sheksper@gmail.com" };
            var postResponse = await _client.PostAsJsonAsync("api/users", createUser, ct);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdUser = await postResponse.Content.ReadFromJsonAsync<User>(ct);
            createdUser.Should().NotBeNull();
            createdUser!.Email.Should().Be(createUser.Email);

            var getUser = await _client.GetAsync($"api/users/{createdUser.Id}", ct);
            getUser.StatusCode.Should().Be(HttpStatusCode.OK);

            var getResponse = await getUser.Content.ReadFromJsonAsync<UserManagement>(cancellationToken: ct);
            getResponse!.Id.Should().Be(createdUser.Id);
            getResponse.FirstName.Should().Be(createUser.FirstName);
            getResponse.LastName.Should().Be(createUser.LastName);

        }

    }
}
