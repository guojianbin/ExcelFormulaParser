﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelFormulaParser.Engine.ExpressionGraph;
using ExcelFormulaParser.Engine.Exceptions;

namespace ExcelFormulaParser.Engine.Excel.Functions.Information
{
    public class IsError : ErrorHandlingFunction
    {
        public override CompileResult Execute(IEnumerable<FunctionArgument> arguments, ParsingContext context)
        {
            if (arguments == null || arguments.Count() == 0)
            {
                return CreateResult(false, DataType.Boolean);
            }
            foreach (var argument in arguments)
            {
                if (ExcelErrorCodes.IsErrorCode(argument.Value))
                {
                    return CreateResult(true, DataType.Boolean);
                }
            }
            return CreateResult(false, DataType.Boolean);
        }

        public override CompileResult HandleError(string errorCode)
        {
            return CreateResult(true, DataType.Boolean);
        }
    }
}
