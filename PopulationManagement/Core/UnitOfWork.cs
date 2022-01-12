// <copyright file="UnitOfWork.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core
{
    using System;
    using Core.Repositories;
    using DataAccess;
    using Models;

    /// <summary>
    /// Unit of work class - ensure data consistency.
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool disposed = false;
        private PopulationDbContext context;
        private IBaseRepository<Actual> actualRepository;
        private IEstimateRepository estimateRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Injected context.</param>
        public UnitOfWork(PopulationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets actual repository.
        /// </summary>
        public IBaseRepository<Actual> ActualRepository
        {
            get
            {
                if (this.actualRepository == null)
                {
                    this.actualRepository = new BaseRepository<Actual>(this.context);
                }

                return this.actualRepository;
            }
        }

        /// <summary>
        /// Gets estimate repository.
        /// </summary>
        public IEstimateRepository EstimateRepository
        {
            get
            {
                if (this.estimateRepository == null)
                {
                    this.estimateRepository = new EstimateRepository(this.context);
                }

                return this.estimateRepository;
            }
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void Save()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
