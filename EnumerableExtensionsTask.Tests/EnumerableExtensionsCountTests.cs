using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace EnumerableExtensionsTask.Tests
{
    [TestFixture]
    [Category("Cast")]
    public class EnumerableExtensionsCountTests
    {
         private static IEnumerable<TestCaseData> TestCasesDataForCount_ValueType
         {
            get
            {
                yield return new TestCaseData(new int[] { 1, 4, 5, 6, 7, 8, 9, -123, 45, 12 }, 10);
                yield return new TestCaseData(Array.Empty<int>(), 0);
            }
         }

         private static IEnumerable<TestCaseData> TestCasesDataForCount_ReferenceType
        {
            get
            {
                yield return new TestCaseData(new string[] { "abc", "vdf", "lalalal", null }, 4);
                yield return new TestCaseData(new object[] { "abc", 2, 3.3d, null }, 4);
            }
        }

         private static IEnumerable<TestCaseData> TestCasesDataForCountWithPredicate_ValueType
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 4, 5, 6, 7, 8, 9, -123, 45, 12 }, 9, new Func<int, bool>((x) => x > 0));
                yield return new TestCaseData(Array.Empty<int>(), 0, new Func<int, bool>((x) => x == 0));
            }
        }

         private static IEnumerable<TestCaseData> TestCasesDataForCountWithPredicate_ReferenceType
        {
            get
            {
                yield return new TestCaseData(new string[] { "abc", "vdf", "lalalal", null }, 1, new Func<object, bool>((x) => x is null));
                yield return new TestCaseData(new object[] { "abc", 2, 3.3d, null }, 1, new Func<object, bool>((x) => x is int));
            }
        }

         [TestCaseSource(nameof(TestCasesDataForCount_ValueType))]
         public void Count_ValueType(IEnumerable<int> source, int expected) =>
            Assert.AreEqual(expected, source.Count());

         [TestCaseSource(nameof(TestCasesDataForCount_ReferenceType))]
         public void Count_ReferenceType(IEnumerable<object> source, int expected) =>
            Assert.AreEqual(expected, source.Count());

         [TestCase(null, null)]
         public void CountWithPredicate_SourceIsNull_ThrowASrgumentNullException(IEnumerable<int> source, Func<int, bool> predicate) =>
           Assert.Throws<ArgumentNullException>(() => source.Count(predicate));

         [TestCase(new int[] { 1, 2 }, null)]
         public void CountWithPredicate_PredicateIsNull_ThrowASrgumentNullException(IEnumerable<int> source, Func<int, bool> predicate) =>
          Assert.Throws<ArgumentNullException>(() => source.Count(predicate));

         [TestCaseSource(nameof(TestCasesDataForCountWithPredicate_ValueType))]
         public void CountWithPredicate_ValueType(IEnumerable<int> source, int expected, Func<int, bool> predicate) =>
            Assert.AreEqual(expected, source.Count(predicate));

         [TestCaseSource(nameof(TestCasesDataForCountWithPredicate_ReferenceType))]
         public void CountWithPredicate_ReferenceType(IEnumerable<object> source, int expected, Func<object, bool> predicate) =>
           Assert.AreEqual(expected, source.Count(predicate));

         [TestCase(null)]
         public void Count_SourceIsNull_ThrowASrgumentNullException(IEnumerable<int> source) =>
           Assert.Throws<ArgumentNullException>(() => source.Count());
    }
}
