using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    public abstract class Expression
    {
        public abstract Expression Exchange(string varName, Expression replacement);
        public abstract string ToMathString();
        public abstract string ToHumanString();
        public abstract List<Expression> GetDefinednessConditions();
        public abstract double Evaluate(Dictionary<string, double> variables);
        public virtual bool EvaluateBool(Dictionary<string, double> variables)
        {
            return Evaluate(variables) != 0;
        }
        public virtual Expression Simplify() => this;

    }  
}
