// <copyright file="IPopulationService.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Services
{
    using Models;

    /// <summary>
    /// IPopulationService.
    /// </summary>
    public interface IPopulationService
    {
        /// <summary>
        /// Get population of many states in one time.
        /// </summary>
        /// <param name="states">List of state id.</param>
        /// <returns>List of states and it's population if exists, otherwise error.</returns>
        ServiceResult GetPopulationOfManyStates(int[] states);
    }
}