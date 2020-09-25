using Crudify.Internals;
using Crudify.Modelling;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Crudify
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrud<TDbContext>(this IServiceCollection serviceCollection, Action<CrudOptionsBuilder> options)
            where TDbContext : DbContext
        {
            var builder = new CrudOptionsBuilder();
            options(builder);
            var crudModels = builder.Build();

            serviceCollection
                .AddMvcCore(setup
                    => setup.Conventions.Add(new GenericControllerRouteConvention(crudModels)))
                .ConfigureApplicationPartManager(manager
                    => manager.FeatureProviders.Add(new GenericTypeControllerFeatureProvider<TDbContext>(crudModels)));

            return serviceCollection;
        }
    }
}
