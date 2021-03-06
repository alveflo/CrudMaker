﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudMaker.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class, IIdentity
    {
        /// <summary>
        /// Adds an entity.
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a single entity, querying by provided id object.
        /// If no entity is found, null is returned.
        /// </summary>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Returns a queryable of the entity collection.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// Updates an entity and returns an updated object.
        /// </summary>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
