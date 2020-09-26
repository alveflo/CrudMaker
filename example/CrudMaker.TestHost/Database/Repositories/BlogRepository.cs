using CrudMaker.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudMaker.TestHost.Database.Repositories
{
    public class BlogRepository : IRepository<Blog>
    {
        private readonly TestDbContext _dbContext;

        public BlogRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Blog> AddAsync(Blog entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Blog entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Blog> GetQueryable()
        {
            return _dbContext.Blogs
                .AsQueryable()
                .Include(x => x.Posts);
        }

        public Task UpdateAsync(Blog entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
