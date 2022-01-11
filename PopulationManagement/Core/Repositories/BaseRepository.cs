// <copyright file="BaseRepository.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccess;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class BaseRepository<T>
        where T : class
    {
        private PopulationDbContext context;
        private DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public BaseRepository(PopulationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// Modified IEnumrable to List - LinhTQ8
        /// <summary>
        /// Get entities with certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="orderBy">Ordered param.</param>
        /// <param name="includeProperties">Eager load references property.</param>
        /// <returns>List of matched entities.</returns>
        public virtual List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Get a entity by Id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>The entity found or null.</returns>
        public virtual T GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>
        /// Get first entity match certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>Found entity or null.</returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Get unique entity match certain conditions.
        /// </summary>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>Found entity or null.</returns>
        public T GetSingleOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Get pagination.
        /// </summary>
        /// <param name="pageSize">Number of entities per page.</param>
        /// <param name="pageIndex">Index of the text to get.</param>
        /// <param name="filter">Lambda expression filter.</param>
        /// <param name="orderBy">Ordered condition.</param>
        /// <param name="includeProperties">Eager load properties.</param>
        /// <returns>List of entities in page.</returns>
        public List<T> GetWithPaging(int pageSize, int pageIndex, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        /// <summary>
        /// Insert entity by passing an instance.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        public virtual void Insert(T entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">Id of the entity to be deleted.</param>
        public virtual void Delete(object id)
        {
            T entityToDelete = this.dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Delete enties.
        /// </summary>
        /// <param name="list">List entity to be deleted.</param>
        public virtual void RemoveRange(IEnumerable<T> list)
        {
            this.dbSet.RemoveRange(list);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to be deleted.</param>
        public virtual void Remove(T entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }

            this.dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to be updated.</param>
        public virtual void Update(T entityToUpdate)
        {
            this.dbSet.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Count number of records with conditions.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Number of matched records.</returns>
        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = this.dbSet;

            if (filter != null)
            {
                return query.Count(filter);
            }

            return this.dbSet.Count();
        }
    }
}
