using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    public class Constant : Expression
    {
        public double Value { get; }

        public Constant(double value)
        {
            Value = value;
        }

        public override Expression Exchange(string varName, Expression replacement)
        {
            return this;
        }

        public override string ToMathString()
        {
            return Value.ToString();
        }

        public override string ToHumanString()
        {
            if (Value == (int)Value) return ((int)Value).ToString();
            return Value.ToString();
        }

        public override List<Expression> GetDefinednessConditions()
        {
            return new List<Expression>();
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            return Value;
        }

        public override Expression Simplify()
        {
            return this;
        }
    }
}
