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
        BaseRepository<Actual> ActualRepository { get; }

        /// <summary>
        /// Gets estimate repository.
        /// </summary>
        EstimateRepository EstimateRepository { get; }

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
