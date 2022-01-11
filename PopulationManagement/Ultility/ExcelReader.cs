// <copyright file="ExcelReader.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Ultility
{
    using System;
    using System.Collections.Generic;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Models;

    /// <summary>
    /// Ultility for reading excel files.
    /// </summary>
    public class ExcelReader : IExcelReader
    {
        /// <summary>
        /// Read excel file into collections.
        /// </summary>
        /// <param name="actuals">Actual stats.</param>
        /// <param name="estimates">Estimate stats.</param>
        /// <param name="filePath">Path to the excel file.</param>
        public void LoadDataFromExcel(ref List<Actual> actuals, ref List<Estimate> estimates, string filePath)
        {
            try
            {
                // Lets open the existing excel file and read through its content . Open the excel using openxml sdk.
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
                {
                    // create the object for workbook part.
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();

                    // using for each loop to get the sheet from the sheetcollection.
                    foreach (Sheet thesheet in thesheetcollection)
                    {
                        // get the worksheet object by using the sheet id.
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;
                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        if (thesheet.Name == "Actuals")
                        {
                            foreach (Row thecurrentrow in thesheetdata)
                            {
                                if (thecurrentrow.RowIndex == 1)
                                {
                                    continue;
                                }

                                int colIndex = 0;
                                var actual = new Actual();
                                foreach (Cell thecurrentcell in thecurrentrow)
                                {
                                    switch (colIndex)
                                    {
                                        case 0:
                                            {
                                                if (int.TryParse(thecurrentcell.InnerText, out int id))
                                                {
                                                    actual.State = id;
                                                }

                                                break;
                                            }

                                        case 1:
                                            {
                                                if (decimal.TryParse(thecurrentcell.InnerText, out decimal population))
                                                {
                                                    actual.Population = population;
                                                }

                                                break;
                                            }

                                        case 2:
                                            {
                                                if (decimal.TryParse(thecurrentcell.InnerText, out decimal households))
                                                {
                                                    actual.Households = households;
                                                }

                                                break;
                                            }

                                        default:
                                            break;
                                    }

                                    colIndex++;
                                }

                                actuals.Add(actual);
                            }
                        }

                        if (thesheet.Name == "Estimates")
                        {
                            foreach (Row thecurrentrow in thesheetdata)
                            {
                                if (thecurrentrow.RowIndex == 1)
                                {
                                    continue;
                                }

                                int colIndex = 0;
                                var estimate = new Estimate();
                                foreach (Cell thecurrentcell in thecurrentrow)
                                {
                                    switch (colIndex)
                                    {
                                        case 0:
                                            {
                                                if (int.TryParse(thecurrentcell.InnerText, out int id))
                                                {
                                                    estimate.State = id;
                                                }

                                                break;
                                            }

                                        case 1:
                                            {
                                                if (int.TryParse(thecurrentcell.InnerText, out int district))
                                                {
                                                    estimate.District = district;
                                                }

                                                break;
                                            }

                                        case 2:
                                            {
                                                if (decimal.TryParse(thecurrentcell.InnerText, out decimal population))
                                                {
                                                    estimate.Population = population;
                                                }

                                                break;
                                            }

                                        case 3:
                                            {
                                                if (decimal.TryParse(thecurrentcell.InnerText, out decimal households))
                                                {
                                                    estimate.Households = households;
                                                }

                                                break;
                                            }

                                        default:
                                            break;
                                    }

                                    colIndex++;
                                }

                                estimates.Add(estimate);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
