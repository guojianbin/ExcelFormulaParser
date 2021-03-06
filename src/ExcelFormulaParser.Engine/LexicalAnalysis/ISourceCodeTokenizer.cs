﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelFormulaParser.Engine.LexicalAnalysis
{
    public interface ISourceCodeTokenizer
    {
        IEnumerable<Token> Tokenize(string input);
    }
}
