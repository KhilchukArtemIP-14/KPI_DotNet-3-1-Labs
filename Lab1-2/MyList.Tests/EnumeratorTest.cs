using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;

namespace MyList.Tests
{
    public class EnumeratorTest
    {
        [Fact]
        public void MoveNext_WhenUsedInside_MustSucceed()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();

            var next = enumerator.MoveNext();

            Assert.True(next);
            Assert.Equal(1,enumerator.Current);
        }

        [Fact]
        public void MoveNext_WhenUsedAtEnd_MustReturnFalse()
        {
            CustomList<int> coll = new CustomList<int>();
            var enumerator = coll.GetEnumerator();

            var next = enumerator.MoveNext();

            Assert.False(next);
        }

        [Fact]
        public void Enumerator_WhenEnumerated_MustEnumerateProperly()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            List<int> expected = new List<int>() {  1, 2, 3, 4  };
            List<int> enumeratedSeuqence = new List<int>();

            foreach(var a in coll)
            {
                enumeratedSeuqence.Add(a);
            }

            Assert.Equal(expected, enumeratedSeuqence);
        }

        [Fact]
        public void Enumerator_WhenVersionChanged_MustThrow()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();

            coll.Add(5);
            Action badMoveNext = () => enumerator.MoveNext();

            var exception = Assert.Throws<Exception>(badMoveNext);
            Assert.Equal("The collection has been modified", exception.Message);
        }
        [Fact]
        public void Reset_WhenUsed_MustThrow()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();

            enumerator.Reset();

            
            Assert.Equal(1, enumerator.Current);
        }

        [Fact]
        public void MoveNext_AfterReachingTheEnd_MustReset()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            var enumerator = coll.GetEnumerator();

            foreach (var a in coll) { }

            Assert.Equal(1, enumerator.Current);
        }
    }
}
