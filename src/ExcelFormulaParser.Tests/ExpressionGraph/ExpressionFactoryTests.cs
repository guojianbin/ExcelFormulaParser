﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelFormulaParser.Engine.ExpressionGraph;
using ExcelFormulaParser.Engine.LexicalAnalysis;

namespace ExcelFormulaParser.Tests.ExpressionGraph
{
    [TestClass]
    public class ExpressionFactoryTests
    {
        private IExpressionFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new ExpressionFactory();
        }

        [TestMethod]
        public void ShouldReturnIntegerExpressionWhenTokenIsInteger()
        {
            var token = new Token("2", TokenType.Integer);
            var expression = _factory.Create(token);
            Assert.IsInstanceOfType(expression, typeof(IntegerExpression));
        }

        [TestMethod]
        public void ShouldReturnDecimalExpressionWhenTokenIsDecimal()
        {
            var token = new Token("2.5", TokenType.Decimal);
            var expression = _factory.Create(token);
            Assert.IsInstanceOfType(expression, typeof(DecimalExpression));
        }
    }
}
