﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Crudify
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCrud<TDto, TEntity, TDbContext>(this IApplicationBuilder applicationBuilder, string path)
            where TDbContext : DbContext
            where TEntity : class
        {
            applicationBuilder.Map(new PathString(path), ab =>
            {
                ab.Run(async (ctx) =>
                {
                    var x = ctx.RequestServices.GetRequiredService<TDbContext>();
                    var y = GetDbSet<TEntity, TDbContext>(x);

                    var method = ctx.Request.Method;

                    switch (method)
                    {
                        case "POST":
                            await ctx.Response.WriteAsync("Map Post Successful");
                            break;
                        case "GET":
                            await ctx.Response.WriteAsync("Map Get Successful");
                            break;
                    }
                });
            });

            return applicationBuilder;
        }

        internal static DbSet<TEntity> GetDbSet<TEntity, TDbContext>(TDbContext ctx)
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
