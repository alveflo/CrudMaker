using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.OData.Edm;

namespace Crudify
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAutoCrudOData(this IApplicationBuilder applicationBuilder)
        {
            var edmModel = (IEdmModel) applicationBuilder.ApplicationServices.GetService(typeof(IEdmModel));

            applicationBuilder.UseOData(edmModel);
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                routeBuilder.MapODataServiceRoute("odata", "odata", edmModel);

                // The following line to Work-around for #1175 in beta1
                routeBuilder.EnableDependencyInjection();
            });

            return applicationBuilder;
        }
    }
}
