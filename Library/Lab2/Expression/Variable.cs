using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lab2.Expression
{
    /// <summary>
    /// Выражение-переменная, представленная именем.
    /// Используется для подстановки значений при вычислении выражений.
    /// </summary>
    public class Variable : Expression
    {
        /// <summary>
        /// Имя переменной.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Создаёт переменную с заданным именем.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public Variable(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Если имя переменной совпадает с <paramref name="varName"/>, возвращает выражение-замену, иначе — саму переменную.
        /// </summary>
        public override Expression Exchange(string varName, Expression replacement)
        {
            if (Name == varName)
                return replacement;
            return this;
        }
        /// <summary>
        /// Возвращает математическое имя переменной.
        /// </summary>
        public override string ToMathString()
        {
            return Name;
        }
        /// <summary>
        /// Возвращает человекочитаемое имя переменной (по умолчанию совпадает с именем).
        /// </summary>
        public override string ToHumanString()
        {
            return Name;
        }
        /// <summary>
        /// Переменная не накладывает дополнительных условий определённости.
        /// </summary>
        public override List<Expression> GetDefinednessConditions()
        {
            return new List<Expression>();
        }
        /// <summary>
        /// Возвращает значение переменной из словаря значений. Если переменная не найдена, выбрасывает исключение.
        /// </summary>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            if (variables.TryGetValue(Name, out double value))
                return value;
            throw new Exception($"Переменная '{Name}' не определена.");
        }
    }
}
