// <copyright file="IUnitOfWork.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core
{
    using Core.Repositories;
    using Models;

    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets actual repository.
        /// </summary>
        IBaseRepository<Actual> ActualRepository { get; }

        /// <summary>
        /// Gets estimate repository.
        /// </summary>
        IEstimateRepository EstimateRepository { get; }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void Save();

        /// <summary>
        /// Dispose.
        /// </summary>
        void Dispose();
    }
}
