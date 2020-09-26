using CrudMaker.IntegrationTests.Setup;
using CrudMaker.TestHost;
using CrudMaker.TestHost.Database;
using CrudMaker.TestHost.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrudMaker.IntegrationTests.CrudTests
{
    public class GetById_Tests
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
        public async Task When_getting_by_id_and_entity_doesnt_exist_Then_http_status_is_not_found()
        {
            using var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var response = await _client.GetAsync($"api/posts/{Guid.NewGuid()}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task When_deleting_data_Then_data_is_deleted_from_database()
        {
            using var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var existingPost = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Old title",
                Content = "Old content"
            };
            dbContext.Add(existingPost);
            dbContext.SaveChanges();

            var response = await _client.GetAsync($"api/posts/{existingPost.Id}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var objectResult = JsonConvert.DeserializeObject<PostDto>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(objectResult.Id, existingPost.Id);
            Assert.AreEqual(objectResult.Title, existingPost.Title);
            Assert.AreEqual(objectResult.Content, existingPost.Content);
        }
    }
}
