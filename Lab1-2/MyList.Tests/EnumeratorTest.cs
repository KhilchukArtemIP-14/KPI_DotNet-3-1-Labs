using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;

namespace MyList.Tests
{
    public class EnumeratorTest
    {
        [Theory]
        [MemberData(nameof(GetMoveNextValidTestData))]
        public void MoveNext_WhenCanAdvance_MustAdvance(CustomList<int> coll)
        {
            var enumerator = coll.GetEnumerator();

            var next = enumerator.MoveNext();

            Assert.True(next);
            Assert.Equal(1,enumerator.Current);
        }

        [Fact]
        public void MoveNext_WhenUsedBeyondEnd_MustReturnFalse()
        {
            CustomList<int> coll = new CustomList<int>() { 1 };
            var enumerator = coll.GetEnumerator();
            enumerator.MoveNext();

            var next = enumerator.MoveNext();

            Assert.False(next);
        }

        [Fact]
        public void MoveNext_WhenUsedOnEmpty_MustPointAtDefault()
        {
            CustomList<int> coll = new CustomList<int>();
            var enumerator = coll.GetEnumerator();

            var next = enumerator.MoveNext();

            Assert.False(next);
            Assert.Equal(default(int), enumerator.Current);
        }

        [Fact]
        public void MoveNext_AfterReachingTheEnd_MustResetEnumerator()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();

            foreach (var a in coll) { }

            Assert.Equal(1, enumerator.Current);
        }

        [Fact]
        public void Enumerator_WhenTraversingCollection_MustEnumerateProperly()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            List<int> expected = new List<int>() { 1, 2, 3, 4 };
            List<int> traverseSeuqence = new List<int>();

            foreach (var a in coll)
            {
                traverseSeuqence.Add(a);
            }

            Assert.Equal(expected, traverseSeuqence);
        }

        [Fact]
        public void Enumerator_WhenCollectionIsEmpty_MustPointAtDefault()
        {
            CustomList<int> coll = new CustomList<int>();

            var enumerator = coll.GetEnumerator();

            Assert.Equal(default(int), enumerator.Current);
        }

        [Fact]
        public void Enumerator_WhenVersionChanged_MustThrow()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();
            coll.Add(5);

            Action moveNextAfterChange = () => enumerator.MoveNext();

            var exception = Assert.Throws<Exception>(moveNextAfterChange);
            Assert.Equal("The collection has been modified", exception.Message);
        }

        [Fact]
        public void Reset_WhenUsed_MustPointAtBeginning()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();

            enumerator.Reset();
            
            Assert.Equal(1, enumerator.Current);
        }

        [Fact]
        public void Reset_WhenCollectionIsEmpty_EnumeratorMustPointAtDefault()
        {
            CustomList<int> coll = new CustomList<int>();
            var enumerator = coll.GetEnumerator();

            enumerator.Reset();

            Assert.Equal(default(int), enumerator.Current);
        }

        public static IEnumerable<object[]> GetMoveNextValidTestData()
        {
            yield return new object[] { new CustomList<int>() { 1, 2, 3, 4, 5 } };
            yield return new object[] { new CustomList<int>() { 1 } };
        }
    }
}
