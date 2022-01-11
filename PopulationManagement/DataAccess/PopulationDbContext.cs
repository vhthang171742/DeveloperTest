// <copyright file="PopulationDbContext.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace DataAccess
{
    using System;
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// Database context.
    /// </summary>
    public class PopulationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationDbContext"/> class.
        /// </summary>
        public PopulationDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public PopulationDbContext(DbContextOptions<PopulationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets Actual dbset.
        /// </summary>
        public DbSet<Actual> Actuals { get; set; }

        /// <summary>
        /// Gets or sets Estimate dbset.
        /// </summary>
        public DbSet<Estimate> Estimates { get; set; }

        /// <summary>
        /// Configure options.
        /// </summary>
        /// <param name="optionsBuilder">Options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlitePath = Path.Combine(
                    Environment.CurrentDirectory + @"\..\",
                    @"API\PopulationManagement.db");
                optionsBuilder.UseSqlite("Data Source=" + sqlitePath);
            }
        }

        /// <summary>
        /// Configure models on creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actual>().ToTable("Actuals");
            modelBuilder.Entity<Estimate>().ToTable("Estimates");
            modelBuilder.Entity<Estimate>().HasKey(e => new { e.State, e.District });
        }
    }
}
