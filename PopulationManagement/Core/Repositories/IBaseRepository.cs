// <copyright file="IBaseRepository.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public interface IBaseRepository<T>
        where T : class
    {
        /// Modified IEnumrable to List - LinhTQ8
        /// <summary>
        /// Get entities with certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="orderBy">Ordered param.</param>
        /// <param name="includeProperties">Eager load references property.</param>
        /// <returns>List of matched entities.</returns>
        public List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Get a entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>The entity found or null.</returns>
        public T GetById(object id);

        /// <summary>
        /// Get first entity match certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>Found entity or null.</returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        /// <summary>
        /// Get unique entity match certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>Found entity or null.</returns>
        public T GetSingleOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        /// <summary>
        /// Get pagination.
        /// </summary>
        /// <param name="pageSize">Number of entities per page.</param>
        /// <param name="pageIndex">Index of the text to get.</param>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="orderBy">Ordered condition.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>List of entities in page.</returns>
        public List<T> GetWithPaging(int pageSize, int pageIndex, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);

        /// <summary>
        /// Insert entity by passing an instance.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        public void Insert(T entity);

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">Id of the entity to be deleted.</param>
        public void Delete(object id);

        /// <summary>
        /// Delete enties.
        /// </summary>
        /// <param name="list">List entity to be deleted.</param>
        public void RemoveRange(IEnumerable<T> list);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to be deleted.</param>
        public void Remove(T entityToDelete);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to be updated.</param>
        public void Update(T entityToUpdate);

        /// <summary>
        /// Count number of records with conditions.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Number of matched records.</returns>
        public int Count(Expression<Func<T, bool>> filter = null);
    }
}