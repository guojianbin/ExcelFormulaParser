﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelFormulaParser.Engine.Excel.Functions;

namespace ExcelFormulaParser.Engine.LexicalAnalysis
{
    public class SourceCodeTokenizer : ISourceCodeTokenizer
    {
        public SourceCodeTokenizer(FunctionRepository functionRepository, NameValueProvider nameValueProvider)
            : this(new TokenFactory(functionRepository, nameValueProvider), new TokenSeparatorProvider())
        {

        }
        public SourceCodeTokenizer(ITokenFactory tokenFactory, ITokenSeparatorProvider tokenProvider)
        {
            _tokenFactory = tokenFactory;
            _tokenProvider = tokenProvider;
        }

        private readonly ITokenSeparatorProvider _tokenProvider;
        private readonly ITokenFactory _tokenFactory;

        public IEnumerable<Token> Tokenize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Enumerable.Empty<Token>();
            }
            var context = new TokenizerContext(input);
            foreach (var c in context.FormulaChars)
            {
                Token tokenSeparator;
                if(CharIsTokenSeparator(c, out tokenSeparator))
                {
                    if (context.IsInString && tokenSeparator.TokenType != TokenType.String)
                    {
                        context.AppendToCurrentToken(c);
                        continue;
                    }
                    // two operators in sequence could be "<=" or ">="
                    if (IsPartOfMultipleCharSeparator(context, c))
                    {
                        context.AppendToLastToken(c.ToString());
                        context.NewToken();
                        continue;
                    }
                    if (tokenSeparator.TokenType == TokenType.String)
                    {
                        if (context.LastToken != null &&
                            context.LastToken.TokenType == TokenType.String && 
                            !context.CurrentTokenHasValue)
                        {
                            // We are dealing with an empty string ('').
                            context.AddToken(new Token(string.Empty, TokenType.StringContent));
                        }
                        context.ToggleIsInString();
                    }
                    if (context.CurrentTokenHasValue)
                    {
                        context.AddToken(CreateToken(context));
                    }
                    if (tokenSeparator.Value == "-")
                    {
                        if (TokenIsNegator(context))
                        {
                            context.AddToken(new Token("-", TokenType.Negator));
                            continue;
                        }
                    }
                    context.AddToken(tokenSeparator);
                    context.NewToken();
                    continue;
                }
                context.AppendToCurrentToken(c);
            }
            if (context.CurrentTokenHasValue)
            {
                context.AddToken(CreateToken(context));
            }
            return context.Result;
        }

        private static bool TokenIsNegator(TokenizerContext context)
        {
            return context.LastToken == null
                                        ||
                                        context.LastToken.TokenType == TokenType.Operator
                                        ||
                                        context.LastToken.TokenType == TokenType.OpeningBracket
                                        ||
                                        context.LastToken.TokenType == TokenType.Comma
                                        ||
                                        context.LastToken.TokenType == TokenType.OpeningEnumerable;
        }

        private bool IsPartOfMultipleCharSeparator(TokenizerContext context, char c)
        {
            var lastToken = context.LastToken != null ? context.LastToken.Value : string.Empty;
            return _tokenProvider.IsOperator(lastToken) 
                && _tokenProvider.IsPossibleLastPartOfMultipleCharOperator(c.ToString()) 
                && !context.CurrentTokenHasValue;
        }

        private Token CreateToken(TokenizerContext context)
        {
            if (context.CurrentToken == "-")
            {
                if (context.LastToken == null && context.LastToken.TokenType == TokenType.Operator)
                {
                    return new Token("-", TokenType.Negator);
                }
            }
            return _tokenFactory.Create(context.Result, context.CurrentToken);
        }

        private bool CharIsTokenSeparator(char c, out Token token)
        {
            var result = _tokenProvider.Tokens.ContainsKey(c.ToString());
            token = result ? token = _tokenProvider.Tokens[c.ToString()] : null;
            return result;
        }
    }
}
