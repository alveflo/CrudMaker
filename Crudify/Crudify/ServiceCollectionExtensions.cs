using Crudify.Internals;
using Crudify.Modelling;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crudify
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrud(this IServiceCollection serviceCollection, Action<CrudOptionsBuilder> options)
        {
            var builder = new CrudOptionsBuilder();
            options(builder);
            var crudModels = builder.Build();

            serviceCollection
                .AddMvcCore(setup
                    => setup.Conventions.Add(new GenericControllerRouteConvention(crudModels)))
                .ConfigureApplicationPartManager(manager
                    => manager.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(crudModels)));

            return serviceCollection;
        }
    }
}
