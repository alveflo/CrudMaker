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
    public class Get_Tests
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
        public async Task When_getting_data_Then_data_is_returned()
        {
            using var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var blog = new Blog { Id = Guid.NewGuid(), Url = "url", Rating = 1 };
            var post1 = new Post { Id = Guid.NewGuid(), Title = "Title", Content = "Content", BlogId = blog.Id };
            var post2 = new Post { Id = Guid.NewGuid(), Title = "Title", Content = "Content", BlogId = blog.Id };

            dbContext.Blogs.Add(blog);
            dbContext.Posts.Add(post2);
            dbContext.Posts.Add(post1);
            dbContext.SaveChanges();

            var response = await _client.GetAsync("api/posts");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var objectResult = JsonConvert.DeserializeObject<IEnumerable<PostDto>>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(2, objectResult.Count());
        }


        [Test]
        public async Task When_getting_data_Then_odata_query_is_applied()
        {
            using var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var blog = new Blog { Id = Guid.NewGuid(), Url = "url", Rating = 1 };
            var post1 = new Post { Id = Guid.NewGuid(), Title = "Title1", Content = "Content", BlogId = blog.Id };
            var post2 = new Post { Id = Guid.NewGuid(), Title = "Title2", Content = "Content", BlogId = blog.Id };

            dbContext.Blogs.Add(blog);
            dbContext.Posts.Add(post2);
            dbContext.Posts.Add(post1);
            dbContext.SaveChanges();

            var response = await _client.GetAsync("api/posts?$filter=Title eq 'Title1'");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var objectResult = JsonConvert.DeserializeObject<IEnumerable<PostDto>>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(1, objectResult.Count());
            Assert.AreEqual("Title1", objectResult.Single().Title);
        }
    }
}
