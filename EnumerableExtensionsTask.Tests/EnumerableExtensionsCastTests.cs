using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Count")]
    public class EnumerableExtensionsCastTests
    {
        private static IEnumerable<TestCaseData> TestCasesDataForCast_ValueType
        {
            get
            {
                yield return new TestCaseData(new List<object> { 1, 4, 5, 6, 7, 8, 9, 123, 45, 12 });
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForCast_ReferenceType
        {
            get
            {
                yield return new TestCaseData(new List<string> { "abc", "vdf", "lalalal", null });
                yield return new TestCaseData(new List<object> { "abc", null });
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForCast_InvalidOperation
        {
            get
            {
                yield return new TestCaseData(new List<object> { "abc", "vdf", "lalalal", null, 5, 6, 7 });
                yield return new TestCaseData(new List<object> { "abc", null, 11 });
            }
        }

        private static IEnumerable<TestCaseData> TestCasesDataForCast_AddElement_ValueType
        {
            get
            {
                yield return new TestCaseData(new List<object> { 1, 4, 5, 6, 7, 8, 9, -123, 45, 12 }, 10);
                yield return new TestCaseData(new List<object>(), 1);
            }
        }

        [TestCaseSource(nameof(TestCasesDataForCast_ValueType))]
        public void CastToInt_ValueType(IEnumerable<object> source)
        {
            var actual = source.Cast<int>();
            Assert.IsTrue(actual is IEnumerable<int>);
        }

        [TestCaseSource(nameof(TestCasesDataForCast_ReferenceType))]
        public void CastToString_ReferenceType(IEnumerable<object> source)
        {
            var actual = source.Cast<string>();
            Assert.IsTrue(actual is IEnumerable<string>);
        }

        [TestCase(null)]
        public void Cast_SourceIsNull_ThrowASrgumentNullException(IEnumerable<int> source) =>
          Assert.Throws<ArgumentNullException>(() => source.Cast<int>());

        [TestCaseSource(nameof(TestCasesDataForCast_InvalidOperation))]
        public void Cast_InvalidToCastToString_ThrowInvalidCastException(IEnumerable<object> source) =>
         Assert.Throws<InvalidCastException>(() => source.Cast<string>());

        [TestCaseSource(nameof(TestCasesDataForCast_AddElement_ValueType))]
        public void Cast_InvalidToCastToString_AddInvalidItem_ThrowInvalidCastException(List<object> source, object item)
        {
            var casted = source.Cast<int>();
#pragma warning disable CA1062 // Validate arguments of public methods
            source.Add(item);
            Assert.IsTrue(casted is IEnumerable<int>);
            var count = new List<int>();
            count.AddRange(casted);
            Assert.AreEqual(count.Count, source.Count);
        }
    }
}
