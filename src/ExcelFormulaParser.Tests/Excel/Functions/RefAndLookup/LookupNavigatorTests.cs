﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelFormulaParser.Engine.Excel.Functions.RefAndLookup;
using Rhino.Mocks;
using ExcelFormulaParser.Engine;
using ExcelFormulaParser.Engine.ExcelUtilities;
using ExcelFormulaParser.Engine.Excel.Functions;
using ExcelFormulaParser.Tests.TestHelpers;

namespace ExcelFormulaParser.Tests.Excel.Functions.RefAndLookup
{
    [TestClass]
    public class LookupNavigatorTests
    {
        private LookupArguments GetArgs(params object[] args)
        {
            var lArgs = FunctionsHelper.CreateArgs(args);
            return new LookupArguments(lArgs);
        }

        [TestMethod]
        public void CurrentValueShouldBeFirstCell()
        {
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(0, 0)).Return(new ExcelCell(3, null, 0, 0));
            provider.Stub(x => x.GetCellValue(1, 0)).Return(new ExcelCell(4, null, 0, 0));
            var args = GetArgs(3, "A1:B2", 1);
            var navigator = new LookupNavigator(LookupDirection.Vertical, args, provider);
            Assert.AreEqual(3, navigator.CurrentValue);
        }

        [TestMethod]
        public void MoveNextShouldReturnFalseIfLastCell()
        {
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(0, 0)).Return(new ExcelCell(3, null, 0, 0));
            provider.Stub(x => x.GetCellValue(1, 0)).Return(new ExcelCell(4, null, 0, 0));
            var args = GetArgs(3, "A1:B1", 1);
            var navigator = new LookupNavigator(LookupDirection.Vertical, args, provider);
            Assert.IsFalse(navigator.MoveNext());
        }

        [TestMethod]
        public void HasNextShouldBeTrueIfNotLastCell()
        {
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(0, 0)).Return(new ExcelCell(3, null, 0, 0));
            provider.Stub(x => x.GetCellValue(1, 0)).Return(new ExcelCell(4, null, 0, 0));
            var args = GetArgs(3, "A1:B2", 1);
            var navigator = new LookupNavigator(LookupDirection.Vertical, args, provider);
            Assert.IsTrue(navigator.MoveNext());
        }

        [TestMethod]
        public void MoveNextShouldNavigateVertically()
        {
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(0, 0)).Return(new ExcelCell(3, null, 0, 0));
            provider.Stub(x => x.GetCellValue(1, 0)).Return(new ExcelCell(4, null, 0, 0));
            var args = GetArgs(6, "A1:B2", 1);
            var navigator = new LookupNavigator(LookupDirection.Vertical, args, provider);
            navigator.MoveNext();
            Assert.AreEqual(4, navigator.CurrentValue);
        }

        [TestMethod]
        public void GetLookupValueShouldReturnCorrespondingValue()
        {
            var provider = MockRepository.GenerateStub<ExcelDataProvider>();
            provider.Stub(x => x.GetCellValue(0, 0)).Return(new ExcelCell(3, null, 0, 0));
            provider.Stub(x => x.GetCellValue(0, 1)).Return(new ExcelCell(4, null, 0, 0));
            var args = GetArgs(6, "A1:B2", 2);
            var navigator = new LookupNavigator(LookupDirection.Vertical, args, provider);
            Assert.AreEqual(4, navigator.GetLookupValue());
        }
    }
}
