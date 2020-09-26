using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crudify.Internals
{
    internal class GenericODataController<TEntity, TDbContext> : ODataController
        where TEntity : class, IIdentity
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public GenericODataController(TDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[EnableQuery]
        //[ODataRoute("posts")]
        //public DbSet<TEntity> Get()
        //{
        //    var dbSet = DbContextAccessor.GetDbSet<TEntity, TDbContext>(_context);

        //    return dbSet;
        //}
    }
}
