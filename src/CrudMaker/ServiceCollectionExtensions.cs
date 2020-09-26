using CrudMaker.Abstractions;
using CrudMaker.Internals;
using CrudMaker.Modelling;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CrudMaker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrud<TDbContext>(this IServiceCollection serviceCollection, Action<CrudOptionsBuilder> options)
            where TDbContext : DbContext
        {
            var builder = new CrudOptionsBuilder();
            options(builder);
            var crudModels = builder.Build();

            foreach (var model in crudModels)
            {
                var repository = model.Repository ?? typeof(GenericRepository<,>).MakeGenericType(model.EntityType, typeof(TDbContext));

                serviceCollection
                    .AddScoped(
                        typeof(IRepository<>).MakeGenericType(model.EntityType),
                        repository);
            }

            serviceCollection
                .AddMvcCore(setup
                    => setup.Conventions.Add(new GenericControllerRouteConvention(crudModels)))
                .ConfigureApplicationPartManager(manager
                    => manager.FeatureProviders.Add(new GenericTypeControllerFeatureProvider<TDbContext>(crudModels)));

            serviceCollection.AddOData();

            return serviceCollection;
        }
    }
}
