using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Crudify.TestHost.Database
{
    public class TestDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
        }
    }

    public class Blog : IIdentity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post : IIdentity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
