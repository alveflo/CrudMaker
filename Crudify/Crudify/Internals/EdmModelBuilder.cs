using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;

namespace Crudify.Internals
{
    internal class EdmModelBuilder
    {
        private readonly IEnumerable<CrudModel> _crudModels;

        public EdmModelBuilder(IEnumerable<CrudModel> crudModels)
        {
            _crudModels = crudModels;
        }

        public IEdmModel Build()
        {
            return GetEdmModel(_crudModels);
        }

        private IEdmModel GetEdmModel(IEnumerable<CrudModel> crudModels)
        {
            var builder = new ODataConventionModelBuilder();

            foreach (var model in crudModels)
            {
                var genericMethod = builder.GetType().GetMethod(nameof(builder.EntitySet)).MakeGenericMethod(model.EntityType);

                genericMethod.Invoke(builder, new[] { model.Path });
            }

            builder.Namespace = typeof(CrudModel).Namespace; //crudModels.First().EntityType.Namespace;
            return builder.GetEdmModel();
        }
    }
}
