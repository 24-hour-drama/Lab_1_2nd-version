using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    /// <summary>
    /// Константное числовое выражение.
    /// Представляет фиксированное числовое значение и ведёт себя как литерал в выражениях.
    /// </summary>
    public class Constant : Expression
    {
        /// <summary>
        /// Числовое значение константы.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Создаёт новую константу с указанным значением.
        /// </summary>
        /// <param name="value">Числовое значение константы.</param>
        public Constant(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Замена переменной в константе не изменяет выражение — возвращается сам объект.
        /// </summary>
        public override Expression Exchange(string varName, Expression replacement)
        {
            return this;
        }

        /// <summary>
        /// Возвращает математическое представление константы.
        /// </summary>
        public override string ToMathString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Возвращает человекочитаемое представление константы (убирает дробную часть для целых значений).
        /// </summary>
        public override string ToHumanString()
        {
            if (Value == (int)Value) return ((int)Value).ToString();
            return Value.ToString();
        }

        /// <summary>
        /// Константа не вносит условий определённости — возвращается пустой список.
        /// </summary>
        public override List<Expression> GetDefinednessConditions()
        {
            return new List<Expression>();
        }

        /// <summary>
        /// Возвращает численное значение константы.
        /// </summary>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return Value;
        }

        /// <summary>
        /// Упрощение константы возвращает саму константу.
        /// </summary>
        public override Expression Simplify()
        {
            return this;
        }
    }
}
