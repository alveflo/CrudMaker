using CrudMaker.IntegrationTests.Setup;
using CrudMaker.TestHost;
using CrudMaker.TestHost.Database;
using CrudMaker.TestHost.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrudMaker.IntegrationTests.CrudTests
{
    public class Post_Tests
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task When_posting_data_Then_data_is_stored_in_database()
        {
            using var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var dto = new PostDto
            {
                Title = "Title",
                Content = "Content"
            };

            var response = await _client.PostAsync("api/posts", new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var postFromDatabase = dbContext.Posts.Single();

            Assert.AreEqual(dto.Title, postFromDatabase.Title);
            Assert.AreEqual(dto.Content, postFromDatabase.Content);
        }
    }
}
