using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;
namespace MyList.Tests
{
    public class RemoveTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        public void Remove_WhenListIsNotNull_MustSucceed(int elementToRemove, bool expectedResult)
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };
            int initialCount = 5;

            var remove = coll.Remove(elementToRemove);

            var resultingCount = expectedResult ? initialCount-1 : initialCount;
            Assert.Equal(expectedResult, remove);
            Assert.Equal(coll.Count, resultingCount);
            Assert.DoesNotContain(elementToRemove, coll);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(3)]
        public void RemoveAt_WhenIndexInRangeBeforeEnd_MustSucceedAndShiftNextOnes(int index)
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };
            int elementToRemove = coll[index];
            int initiallyNextOne = coll[index + 1];
            int initialCount = 5;

            coll.RemoveAt(index);

            Assert.Equal(coll.Count, initialCount - 1);
            Assert.Equal(initiallyNextOne, coll[index]);
            Assert.DoesNotContain(elementToRemove, coll);
        }

        [Theory]
        [MemberData(nameof(GetRemoveAtTheEndData))]
        public void RemoveAt_WhenIndexAtEnd_MustSucceed(CustomList<int> coll, int index)
        {
            int elementToRemove = coll[index];
            int initialCount = coll.Count;

            coll.RemoveAt(index);

            Assert.Equal(coll.Count, initialCount - 1);
            Assert.DoesNotContain(elementToRemove, coll);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void RemoveAt_WhenIndexNotInRange_MustThrow(int index)
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };

            Action removeAt = () => coll.RemoveAt(index);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(removeAt);
            Assert.Equal("Argument was out of range", exception.ParamName);
        }
        public static IEnumerable<object[]> GetRemoveAtTheEndData()
        {
            yield return new object[] { new CustomList<int> { 1, 2, 3, 4, 5 }, 4 };
            yield return new object[] { new CustomList<int> { 1 }, 0 };
        }
    }
}
