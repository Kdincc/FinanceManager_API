namespace Task11.Tests.Integration
{
    public class IntegrationTestBase : IClassFixture<FinanceWebApplicationFactory>
    {
        protected readonly FinanceWebApplicationFactory _factory;
        protected HttpClient _client;

        public IntegrationTestBase(FinanceWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
    }
}
