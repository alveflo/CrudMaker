using Crudify.Abstractions;
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

        public CrudOptionsBuilder Add<TDto, TEntity, TRepository>(string path)
            where TDto : class, IIdentity
            where TEntity : class, IIdentity
            where TRepository : IRepository<TEntity>
        {
            _crudModels.Add(new CrudModel
            {
                Path = path,
                DtoType = typeof(TDto),
                EntityType = typeof(TEntity),
                Repository = typeof(TRepository)
            });

            return this;
        }

        public CrudOptionsBuilder Add<TDto, TEntity>(string path)
            where TDto : class, IIdentity
            where TEntity : class, IIdentity
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
