// <copyright file="Actual.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Actual population stats.
    /// </summary>
    public class Actual
    {
        /// <summary>
        /// Gets or sets state's Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State { get; set; }

        /// <summary>
        /// Gets or sets actual state's population.
        /// </summary>
        [Required]
        [Column(TypeName = "Decimal(18,10)")]
        public decimal Population { get; set; }

        /// <summary>
        /// Gets or sets actual state's households.
        /// </summary>
        [Required]
        [Column(TypeName = "Decimal(18,10)")]
        public decimal Households { get; set; }
    }
}
