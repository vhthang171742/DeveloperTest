// <copyright file="Estimate.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Estimate stats.
    /// </summary>
    public class Estimate
    {
        /// <summary>
        /// Gets or sets state's Id.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State { get; set; }

        /// <summary>
        /// Gets or sets district's Id.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int District { get; set; }

        /// <summary>
        /// Gets or sets estimate state's population.
        /// </summary>
        [Required]
        [Column(TypeName = "Decimal(18,10)")]
        public decimal Population { get; set; }

        /// <summary>
        /// Gets or sets estimate state's households.
        /// </summary>
        [Required]
        [Column(TypeName = "Decimal(18,10)")]
        public decimal Households { get; set; }
    }
}
