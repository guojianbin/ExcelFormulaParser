﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using ExcelFormulaParser.Engine.Utilities;

namespace ExcelFormulaParser.Engine.Excel.Functions
{
    public class DoubleArgumentParser : ArgumentParser
    {
        public override object Parse(object obj)
        {
            Require.That(obj).Named("argument").IsNotNull();
            if (obj is double) return obj;
            if (obj.IsNumeric()) return Convert.ToDouble(obj);
            var str = obj != null ? obj.ToString() : string.Empty;
            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            if (decimalSeparator == ",")
            {
                str = str.Replace('.', ',');
            }
            return double.Parse(str);
        }
    }
}
