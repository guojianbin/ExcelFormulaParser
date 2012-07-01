﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelFormulaParser.Engine.VBA.Functions;
using ExcelFormulaParser.Engine.VBA.Functions.Numeric;
using ExcelFormulaParser.Engine;
using ExcelFormulaParser.Tests.TestHelpers;

namespace ExcelFormulaParser.Tests.VBA.Functions
{
    [TestClass]
    public class NumberFunctionsTests
    {
        private ParsingContext _parsingContext;

        [TestMethod]
        public void CIntShouldConvertTextToInteger()
        {
            var func = new CInt();
            var args = FunctionsHelper.CreateArgs("2");
            var result = func.Execute(args, _parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void IntShouldConvertDecimalToInteger()
        {
            var func = new CInt();
            var args = FunctionsHelper.CreateArgs(2.88m);
            var result = func.Execute(args, _parsingContext);
            Assert.AreEqual(2, result.Result);
        }

        [TestMethod]
        public void IntShouldConvertNegativeDecimalToInteger()
        {
            var func = new CInt();
            var args = FunctionsHelper.CreateArgs(-2.88m);
            var result = func.Execute(args, _parsingContext);
            Assert.AreEqual(-3, result.Result);
        }

        [TestMethod]
        public void IntShouldConvertStringToInteger()
        {
            var func = new CInt();
            var args = FunctionsHelper.CreateArgs("-2.88");
            var result = func.Execute(args, _parsingContext);
            Assert.AreEqual(-3, result.Result);
        }
    }
}
