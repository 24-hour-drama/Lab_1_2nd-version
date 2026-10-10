using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    /// <summary>
    /// Унарная операция над одним операндом (например, унарный минус, абсолютное значение, логическое отрицание).
    /// </summary>
    public class UnaryOperation : Expression
    {
        /// <summary>
        /// Операнд унарной операции.
        /// </summary>
        public Expression Operand { get; }
        /// <summary>
        /// Строковое представление оператора ("-", "abs", "!").
        /// </summary>
        public string Operator { get; }
        public UnaryOperation(Expression operand, string op)
        {
            Operand = operand;
            Operator = op;
        }
        /// <summary>
        /// Возвращает новое выражение с выполненной заменой переменной внутри операнда.
        /// </summary>
        public override Expression Exchange(string varName, Expression replacement)
        {
            return new UnaryOperation(Operand.Exchange(varName, replacement), Operator);
        }
        /// <summary>
        /// Возвращает математическое строковое представление унарной операции.
        /// </summary>
        public override string ToMathString()
        {
            return $"({Operator}{Operand.ToMathString()})";
        }
        /// <summary>
        /// Возвращает человекочитаемое строковое представление унарной операции на русском языке.
        /// </summary>
        public override string ToHumanString()
        {
            string opHuman = Operator switch
            {
                "-" => "минус",
                "abs" => "модуль от",
                "!" => "не",
                _ => Operator
            };
            return $"({opHuman} {Operand.ToHumanString()})";
        }
        /// <summary>
        /// Возвращает условия определённости операнда (если есть).
        /// </summary>
        public override List<Expression> GetDefinednessConditions()
        {
            return Operand.GetDefinednessConditions();
        }
        /// <summary>
        /// Вычисляет численное значение унарной операции для заданных значений переменных.
        /// </summary>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            double operandValue = Operand.Evaluate(variables);
            return Operator switch
            {
                "-" => -operandValue,
                "abs" => Math.Abs(operandValue),
                _ => throw new Exception($"Неподдерживаемый унарный оператор '{Operator}'."),
            };
        }

        /// <summary>
        /// Вычисляет логическое значение унарной операции (поддерживает оператор "!").
        /// </summary>
        public override bool EvaluateBool(Dictionary<string, double> variables)
        {
            if (Operator == "!")
                return !Operand.EvaluateBool(variables);
            return base.EvaluateBool(variables);
        }

        /// <summary>
        /// Пытается упростить унарную операцию: сворачивает константы и устраняет двойной унарный минус.
        /// </summary>
        public override Expression Simplify()
        {
            var operand = Operand.Simplify();
            if (Operator == "-" && operand is Constant c)
                return new Constant(-c.Value);
            if (Operator == "abs" && operand is Constant c2)
                return new Constant(Math.Abs(c2.Value));
            if (Operator == "-" && operand is UnaryOperation unaryMinus && unaryMinus.Operator == "-")
                return unaryMinus.Operand;
            return new UnaryOperation(operand, Operator);

        }
    }
}
