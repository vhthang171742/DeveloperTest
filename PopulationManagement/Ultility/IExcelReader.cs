// <copyright file="IExcelReader.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Ultility
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Excel reader Interface.
    /// </summary>
    public interface IExcelReader
    {
        /// <summary>
        /// Read excel file into collections.
        /// </summary>
        /// <param name="actuals">Actual stats.</param>
        /// <param name="estimates">Estimate stats.</param>
        /// <param name="filePath">Path to the excel file.</param>
        void LoadDataFromExcel(ref List<Actual> actuals, ref List<Estimate> estimates, string filePath = "D:\\Data\\Downloads\\data.xlsx");
    }
}