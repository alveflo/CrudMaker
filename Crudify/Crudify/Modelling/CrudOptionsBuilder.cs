using Crudify.Internals;
using System.Collections.Generic;

namespace Crudify.Modelling
{
    public class CrudOptionsBuilder
    {
        private readonly List<CrudModel> _crudModels;

        internal CrudOptionsBuilder()
        {
            _crudModels = new List<CrudModel>();
        }

        public CrudOptionsBuilder Add<TDto, TEntity>(string path)
            where TDto : class
            where TEntity : class
        {
            _crudModels.Add(new CrudModel
            {
                Path = path,
                DtoType = typeof(TDto),
                EntityType = typeof(TEntity)
            });

            return this;
        }

        internal List<CrudModel> Build()
        {
            return _crudModels;
        }
    }
}
