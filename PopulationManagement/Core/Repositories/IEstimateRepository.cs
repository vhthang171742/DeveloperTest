// <copyright file="IEstimateRepository.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core.Repositories
{
    using Models;

    /// <summary>
    /// Estimate repository interface.
    /// </summary>
    public interface IEstimateRepository : IBaseRepository<Estimate>
    {
        /// <summary>
        /// Get total population by state.
        /// </summary>
        /// <param name="stateId">State id.</param>
        /// <returns>Total population of the state.</returns>
        decimal GetTotalHouseholdsByState(int stateId);

        /// <summary>
        /// Get total households by state.
        /// </summary>
        /// <param name="stateId">State id.</param>
        /// <returns>Total Households of the state.</returns>
        decimal GetTotalPopulationByState(int stateId);
    }
}