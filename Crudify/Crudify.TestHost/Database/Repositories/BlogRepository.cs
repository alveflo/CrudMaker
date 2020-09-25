using Crudify.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crudify.TestHost.Database.Repositories
{
    public class BlogRepository : IRepository<Blog>
    {
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

        public Task UpdateAsync(Blog entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
