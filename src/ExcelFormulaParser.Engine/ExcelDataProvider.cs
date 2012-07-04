﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelFormulaParser.Engine
{
    /// <summary>
    /// This class should be implemented to be able to deliver excel data
    /// to the formula parser.
    /// </summary>
    public abstract class ExcelDataProvider : IDisposable
    {
        /// <summary>
        /// Returns values from the required range.
        /// </summary>
        /// <param name="address">An Excel address</param>
        /// <returns>values from the required cells</returns>
        public abstract IEnumerable<ExcelDataItem> GetRangeValues(string address);


        public abstract void Dispose();
    }
}
