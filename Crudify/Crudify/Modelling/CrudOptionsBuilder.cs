using Crudify.Internals;
using System;
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

        public CrudOptionsBuilder Add<TDto, TEntity>(string path, Type repository = null)
            where TDto : class, IIdentity
            where TEntity : class, IIdentity
        {
            _crudModels.Add(new CrudModel
            {
                Path = path,
                DtoType = typeof(TDto),
                EntityType = typeof(TEntity),
                Repository = repository
            });

            return this;
        }

        internal List<CrudModel> Build()
        {
            return _crudModels;
        }
    }
}
