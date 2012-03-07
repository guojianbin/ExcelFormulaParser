﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelFormulaParser.Engine.VBA.Functions;

namespace ExcelFormulaParser.Tests.VBA.Functions
{
    [TestClass]
    public class NumberFunctionsTests
    {
        [TestMethod]
        public void CIntShouldConvertTextToInteger()
        {
            var func = new CInt();
            var args = new object[] { "2" };
            var result = func.Execute(args);
            Assert.AreEqual(2, result.Result);
        }
    }
}