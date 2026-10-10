using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    /// <summary>
    /// Базовый абстрактный класс для всех выражений.
    /// Содержит общие операции: подстановка, приведение к строкам, проверка определённости и вычисление.
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// Выполняет замену переменной <paramref name="varName"/> на выражение <paramref name="replacement"/>.
        /// </summary>
        public abstract Expression Exchange(string varName, Expression replacement);
        /// <summary>
        /// Возвращает строковое представление выражения в математической нотации.
        /// </summary>
        public abstract string ToMathString();
        /// <summary>
        /// Возвращает человекочитаемое строковое представление выражения (на русском языке).
        /// </summary>
        public abstract string ToHumanString();
        /// <summary>
        /// Возвращает список условий определённости (например, проверки деления на ноль).
        /// </summary>
        public abstract List<Expression> GetDefinednessConditions();
        /// <summary>
        /// Вычисляет численное значение выражения при заданных значениях переменных.
        /// </summary>
        public abstract double Evaluate(Dictionary<string, double> variables);
        /// <summary>
        /// Вычисляет логическое значение выражения (false при значении 0, иначе true).
        /// По умолчанию использует <see cref="Evaluate"/>.
        /// </summary>
        public virtual bool EvaluateBool(Dictionary<string, double> variables)
        {
            return Evaluate(variables) != 0;
        }
        /// <summary>
        /// Применяет упрощение выражения и возвращает более простую форму или сам объект по умолчанию.
        /// </summary>
        public virtual Expression Simplify() => this;

    }  
}
