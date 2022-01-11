// <copyright file="EstimateRepository.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Core.Repositories
{
    using System.Linq;
    using DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// Estimate repository.
    /// </summary>
    public class EstimateRepository : BaseRepository<Estimate>
    {
        private PopulationDbContext context;
        private DbSet<Estimate> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstimateRepository"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public EstimateRepository(PopulationDbContext context)
            : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Estimate>();
        }

        /// <summary>
        /// Get total population by state.
        /// </summary>
        /// <param name="stateId">State id.</param>
        /// <returns>Total population of the state.</returns>
        public decimal GetTotalPopulationByState(int stateId)
        {
            IQueryable<Estimate> query = this.dbSet;
            return (decimal)query.Where(e => e.State == stateId).Sum(e => (double)e.Population);
        }

        /// <summary>
        /// Get total households by state.
        /// </summary>
        /// <param name="stateId">State id.</param>
        /// <returns>Total Households of the state.</returns>
        public decimal GetTotalHouseholdsByState(int stateId)
        {
            IQueryable<Estimate> query = this.dbSet;
            return (decimal)query.Where(e => e.State == stateId).Sum(e => (double)e.Households);
        }
    }
}
