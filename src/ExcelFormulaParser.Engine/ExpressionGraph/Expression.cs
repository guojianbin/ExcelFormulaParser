﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelFormulaParser.Engine.VBA.Operators;

namespace ExcelFormulaParser.Engine.ExpressionGraph
{
    public abstract class Expression
    {
        protected string ExpressionString { get; private set; }
        private readonly List<Expression> _children = new List<Expression>();
        public IEnumerable<Expression> Children { get { return _children; } }
        public Expression Next { get; set; }
        public Expression Prev { get; set; }
        public IOperator Operator { get; set; }
        public abstract bool IsGroupedExpression { get; }

        public Expression()
        {

        }

        public Expression(string expression)
        {
            ExpressionString = expression;
            Operator = null;
        }

        public virtual void AddChild(Expression child)
        {
            if (_children.Any())
            {
                var last = _children.Last();
                child.Prev = last;
                last.Next = child;
            }
            _children.Add(child);
        }

        //public virtual Expression MergeWithNext()
        //{
        //    if (Next != null && Operator != null)
        //    {
        //        var result = Operator.Apply(Compile(), Next.Compile());
        //        ExpressionString = result.Result.ToString();
        //        if (Next != null)
        //        {
        //            Operator = Next.Operator;
        //        }
        //        else
        //        {
        //            Operator = null;
        //        }
        //        Next = Next.Next;
        //    }
        //    return this;
        //}

        public virtual Expression MergeWithNext()
        {
            var expression = this;
            if (Next != null && Operator != null)
            {
                var result = Operator.Apply(Compile(), Next.Compile());
                expression = ExpressionConverter.Instance.FromCompileResult(result);
                if (Next != null)
                {
                    expression.Operator = Next.Operator;
                }
                else
                {
                    expression.Operator = null;
                }
                expression.Next = Next.Next;
                if (expression.Next != null) expression.Next.Prev = expression;
                expression.Prev = Prev;
            }
            if (Prev != null)
            {
                Prev.Next = expression;
            }
            return expression;
        }

        public abstract CompileResult Compile();


    }
}
