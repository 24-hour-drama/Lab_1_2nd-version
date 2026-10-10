using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    /// <summary>
    /// Двоичная операция над двумя подвыражениями (левым и правым) с заданным оператором.
    /// Поддерживает арифметические и логические операции, а также методы для упрощения и вычисления.
    /// </summary>
    public class BinaryOperation : Expression
    {
        /// <summary>
        /// Левый операнд операции.
        /// </summary>
        public Expression Left { get; }
        /// <summary>
        /// Правый операнд операции.
        /// </summary>
        public Expression Right { get; }
        /// <summary>
        /// Строковое представление оператора (например, "+", "-", "*", "/", ">").
        /// </summary>
        public string Operator { get; }
        public BinaryOperation(Expression left, Expression right, string op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }
        /// <summary>
        /// Возвращает новое выражение, в котором выполнена замена переменной во всех подвыражениях.
        /// </summary>
        public override Expression Exchange(string varName, Expression replacement)
        {
            return new BinaryOperation(Left.Exchange(varName, replacement), Right.Exchange(varName, replacement), Operator);
        }
        /// <summary>
        /// Возвращает математическое строковое представление операции с учётом скобок.
        /// </summary>
        public override string ToMathString()
        {
            return $"({Left.ToMathString()} {Operator} {Right.ToMathString()})";
        }
        /// <summary>
        /// Возвращает человекочитаемое строковое представление операции на русском языке.
        /// </summary>
        public override string ToHumanString()
        {
            string opHuman = Operator switch
            {
                "+" => "плюс",
                "-" => "минус",
                "*" => "умножить на",
                "/" => "разделить на",
                ">" => "строго больше",
                ">=" => "больше или равно",
                "<" => "строго меньше",
                "<=" => "меньше или равно",
                "==" => "равно",
                "!=" => "не равно",
                "&&" => "И",
                "||" => "ИЛИ",
                _ => Operator
            };
            return $"({Left.ToHumanString()} {opHuman} {Right.ToHumanString()})";
        }
        /// <summary>
        /// Возвращает дополнительные условия определённости (напр., проверка деления на ноль).
        /// </summary>
        public override List<Expression> GetDefinednessConditions()
        {
            var conditions = new List<Expression>();
            conditions.AddRange(Left.GetDefinednessConditions());
            conditions.AddRange(Right.GetDefinednessConditions());
            if (Operator == "/")
                conditions.Add(new BinaryOperation(Right, new Constant(0), "!="));
            return conditions;
        }
        /// <summary>
        /// Вычисляет численное значение бинарной операции для заданных значений переменных.
        /// Поддерживаются арифметические операторы: +, -, *, /.
        /// </summary>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            double leftValue = Left.Evaluate(variables);
            double rightValue = Right.Evaluate(variables);
            return Operator switch
            {
                "+" => leftValue + rightValue,
                "-" => leftValue - rightValue,
                "*" => leftValue * rightValue,
                "/" => leftValue / rightValue,
                _ => throw new Exception($"Неизвестный оператор '{Operator}'."),
            };
        }

        /// <summary>
        /// Вычисляет логическое значение бинарной операции (для операторов сравнения и логики).
        /// </summary>
        public override bool EvaluateBool(Dictionary<string, double> variables)
        {
            double leftValue = Left.Evaluate(variables);
            double rightValue = Right.Evaluate(variables);

            return Operator switch
            {
                ">" => leftValue > rightValue,
                ">=" => leftValue >= rightValue,
                "<" => leftValue < rightValue,
                "<=" => leftValue <= rightValue,
                "==" => leftValue == rightValue,
                "!=" => leftValue != rightValue,
                "&&" => (leftValue != 0) && (rightValue != 0),
                "||" => (leftValue != 0) || (rightValue != 0),
                _ => throw new Exception($"Неизвестный логический оператор '{Operator}'."),
            };
        }

        /// <summary>
        /// Пытается упростить выражение: выполняет рекурсивное упрощение операндов и сворачивает константы.
        /// </summary>
        public override Expression Simplify()
        {
            var left = Left.Simplify();
            var right = Right.Simplify();

            if (Operator == "+" && right is Constant rc && rc.Value == 0) return left;
            if (Operator == "+" && left is Constant lc && lc.Value == 0) return right;
            if (Operator == "-" && right is Constant rc2 && rc2.Value == 0) return left;
            if (Operator == "*" && right is Constant rc3 && rc3.Value == 1) return left;
            if (Operator == "*" && left is Constant lc2 && lc2.Value == 1) return right;
            if (Operator == "*" && right is Constant rc4 && rc4.Value == 0) return new Constant(0);
            if (Operator == "*" && left is Constant lc3 && lc3.Value == 0) return new Constant(0);

            if (left is Constant cl && right is Constant cr)
            {
                double result = Operator switch
                {
                    "+" => cl.Value + cr.Value,
                    "-" => cl.Value - cr.Value,
                    "*" => cl.Value * cr.Value,
                    "/" when cr.Value != 0 => cl.Value / cr.Value,
                    _ => double.NaN
                };
                if (!double.IsNaN(result))
                    return new Constant(result);
            }

            return new BinaryOperation(left, right, Operator);

        }
    }
}
