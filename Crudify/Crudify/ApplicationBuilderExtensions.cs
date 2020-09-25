﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Crudify
{
    public class ApplicationBuilderExtensions
    {
        internal DbSet<TEntity> GetDbSet<TEntity, TDbContext>(TDbContext ctx)
            where TDbContext : DbContext
            where TEntity : class
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
