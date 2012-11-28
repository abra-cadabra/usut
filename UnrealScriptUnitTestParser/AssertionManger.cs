using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// The main purpose of the class is to store assertions and test assertion condition using
    /// assertion name and array of values by <see cref="TestAssertion"/> method. 
    /// </summary>
    /// 
    /// <example>
    /// var man = new AssertionManger();
    /// man.Add(new AssertFalse());
    /// man.Add(new AssertTrue());
    /// man.Add(new AssertIntEqual());
    /// var result = man.TestAssertion("AssertIntEqual", new []{"5","25"});
    /// 
    /// </example>
    public class AssertionManger: IEnumerable
    {
        /// <summary>
        /// Dictionary of IAssertion.Name=>IAssertion
        /// </summary>
        private readonly Dictionary<string, IAssertion> _assertionsByName;

         /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count { get { return _assertionsByName.Count; } }

        public AssertionManger()
        {
            _assertionsByName = new Dictionary<string, IAssertion>();
        }

        /// <summary>
        /// Tests values for assertion. Assertion with assertionName should be in AssertionsByName dictionary
        /// </summary>
        /// <param name="assertionName">IAssertion from AssertionsByName dictionary</param>
        /// <param name="values">values to check</param>
        /// <returns>AssertionResul </returns>
        public AssertionInfo TestAssertion(string assertionName, string[] values)
        {
            return _assertionsByName[assertionName].Test(values);
        }

        /// <summary>
        /// Returns known assertions. Use Add and Remove to add or remove assertions to list
        /// </summary>
        /// <returns></returns>
        public IAssertion[] GetAssertions()
        {
            return _assertionsByName.Values.ToArray();
        }
        

        /// <summary>
        /// Add assertion to the list
        /// </summary>
        /// <param name="assertion"></param>
        public void Add(IAssertion assertion)
        {
            _assertionsByName.Add(assertion.Name, assertion);
        }

        /// <summary>
        /// Add assertion to the list
        /// </summary>
        /// <param name="assertion"></param>
        public bool  Remove(IAssertion assertion)
        {
            return _assertionsByName.Remove(assertion.Name);
        }

        /// <summary>
        /// Add assertion to the list
        /// </summary>
        /// <param name="name">Assertion name to remove</param>
        public bool Remove(string name)
        {
            return _assertionsByName.Remove(name);
            
        }

        /// <summary>
        /// Gets IAssertion object by its name. Only if assertion was added by 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IAssertion this[string name]
        {
            get { return _assertionsByName[name]; }
        }
        

        /// <summary>
        /// Clears underlayed assertions dictionary
        /// </summary>
        public void Clear()
        {
            _assertionsByName.Clear();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly { get; private set; }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<string, IAssertion>> GetEnumerator()
        {
            return _assertionsByName.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return  _assertionsByName.GetEnumerator();
        }

        #endregion
    }
}
