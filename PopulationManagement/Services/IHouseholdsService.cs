// <copyright file="IHouseholdsService.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Services
{
    using Models;

    /// <summary>
    /// IHouseholdsService.
    /// </summary>
    public interface IHouseholdsService
    {
        /// <summary>
        /// Get Household of many states in one time.
        /// </summary>
        /// <param name="states">List of state id.</param>
        /// <returns>List of states and it's Household if exists, otherwise error.</returns>
        ServiceResult GetHouseholdsOfManyStates(int[] states);
    }
}