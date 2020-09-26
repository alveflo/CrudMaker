using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CrudMaker
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCrudMakerOData(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                // The following line to Work-around for #1175 in beta1
                routeBuilder.EnableDependencyInjection();
            });

            return applicationBuilder;
        }
    }
}
