using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace Crudify.Internals
{
    internal class GenericTypeControllerFeatureProvider<TDbContext> : IApplicationFeatureProvider<ControllerFeature>
        where TDbContext : DbContext
    {
        private readonly List<CrudModel> _models;

        public GenericTypeControllerFeatureProvider(List<CrudModel> models)
        {
            _models = models;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var model in _models)
            {
                var controller = typeof(GenericController<,,>).MakeGenericType(model.DtoType, model.EntityType, typeof(TDbContext));
                var odataController = typeof(GenericODataController<,>).MakeGenericType(model.EntityType, typeof(TDbContext));

                feature.Controllers.Add(controller.GetTypeInfo());
                feature.Controllers.Add(odataController.GetTypeInfo());
            }
        }
    }
}
