using System.Reflection;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Класс, являющийся родительским для всех тестов, тестирующих объекты.
    /// </summary>
    /// <typeparam name="T">Класс тестируемого объекта</typeparam>
    public abstract class TestOf<T> : TestBase
    {
        #region Fields
        /// <summary>
        /// Точность сравнения целых чисел
        /// </summary>
        protected const double IntegerPrec = 1D;
        /// <summary>
        /// Точность сравнения времени
        /// </summary>
        protected const double TimePrec = 0.01D;
        /// <summary>
        /// Точность стравнения дробных чисел
        /// </summary>
        protected const double FloatPrec = 0.001D;

        #endregion

        #region Properties
        /// <summary>
        /// Тестируемый объект
        /// </summary>
        public T Tester { get; protected set; }

        #endregion

        public TestOf()
        {
            Tester = default(T);
        }

        /// <summary>
        /// Метод, предусмотренный для вызова private-методов тестируемого объекта.
        /// </summary>
        /// <param name="methodName">Имя вызываемого метода.</param>
        /// <returns>Объект, хранящий аттребуты вызываемого метода.</returns>
        protected MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
                Assert.Fail("methodName cannot be null or whitespace");

            var method = Tester.GetType()
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
                Assert.Fail(string.Format("{0} method not found", methodName));

            return method;
        }
    }
}
