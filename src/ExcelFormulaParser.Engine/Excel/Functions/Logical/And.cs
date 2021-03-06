﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelFormulaParser.Engine.ExpressionGraph;

namespace ExcelFormulaParser.Engine.Excel.Functions.Logical
{
    public class And : ExcelFunction
    {
        public override CompileResult Execute(IEnumerable<FunctionArgument> arguments, ParsingContext context)
        {
            ValidateArguments(arguments, 1);
            for (var x = 0; x < arguments.Count(); x++)
            {
                if (!ArgToBool(arguments, x))
                {
                    return new CompileResult(false, DataType.Boolean);
                }
            }
            return new CompileResult(true, DataType.Boolean);
        }
    }
}
