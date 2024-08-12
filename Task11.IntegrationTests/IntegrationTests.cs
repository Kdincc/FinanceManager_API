using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task11.Presentation;

namespace Task11.IntegrationTests
{
    public class IntegrationTests : IClassFixture<Task11WebApllicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public IntegrationTests(Task11WebApllicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetExpenseFinanceOperations_Status200OK()
        {
            var response = await _httpClient.GetAsync("expenses/operations/all");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        }

        [Fact]
        public async Task CreateExpenseFinanceOperation_Status200OK()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "expenses/operations/create")
            {
                Content = new StringContent(
                    @"{
                        ""Date"": ""2022-01-01"",
                        ""ExpenseTypeId"": ""00000000-0000-0000-0000-000000000000"",
                        ""Amount"": 100,
                        ""Description"": ""Test Expense""
                    }",
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        }
    }
}
