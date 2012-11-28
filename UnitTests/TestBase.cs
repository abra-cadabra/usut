using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Родительский класс для тестов, реализующий шаблон TDD теста
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// Инициализация теста
        /// </summary>
        [SetUp]
        public void BaseSetup()
        {
            InitializeSystemUnderTest();
            Setup();
        }
        /// <summary>
        /// Деинициализация теста
        /// </summary>
        [TearDown]
        public void BaseCleanup()
        {
            CleenUp();
        }
        /// <summary>
        /// Initialization of object under test
        /// </summary>
        public abstract void InitializeSystemUnderTest();

        /// <summary>
        /// Test setup. Called prior each test
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// Test cleanup. Called after each test
        /// </summary>
        public abstract void CleenUp();

    }
}
