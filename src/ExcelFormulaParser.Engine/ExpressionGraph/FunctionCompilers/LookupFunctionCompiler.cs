﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelFormulaParser.Engine.Excel.Functions;

namespace ExcelFormulaParser.Engine.ExpressionGraph.FunctionCompilers
{
    public class LookupFunctionCompiler : FunctionCompiler
    {
        public LookupFunctionCompiler(ExcelFunction function)
            : base(function)
        {

        }

        public override CompileResult Compile(IEnumerable<Expression> children, ParsingContext context)
        {
            var args = new List<FunctionArgument>();
            Function.BeforeInvoke(context);
            foreach (var child in children)
            {
                child.ParentIsLookupFunction = Function.IsLookupFuction;
                var arg = child.Compile();
                BuildFunctionArguments(arg != null ? arg.Result : null, args);
            }
            return Function.Execute(args, context);
        }
    }
}
