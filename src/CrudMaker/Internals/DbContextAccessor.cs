using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CrudMaker.Internals
{
    internal static class DbContextAccessor
    {
        internal static DbSet<TEntity> GetDbSet<TEntity, TDbContext>(TDbContext ctx)
            where TDbContext : DbContext
            where TEntity : class, IIdentity
        {
            var properties = ctx.GetType().GetProperties();
            var test = properties.SingleOrDefault(x =>
                x.PropertyType.IsGenericType &&
                x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                x.PropertyType.GenericTypeArguments[0] == typeof(TEntity));

            var x = (DbSet<TEntity>)test.GetValue(ctx);
            return x;
        }
    }
}
