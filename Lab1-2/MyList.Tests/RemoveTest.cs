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
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void Remove_WhenItemInCollection_MustSucceed(int elementToRemove)
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };
            int initialCount = 5;

            var remove = coll.Remove(elementToRemove);

            Assert.True(remove);
            Assert.Equal(coll.Count, initialCount-1);
            Assert.DoesNotContain(elementToRemove,coll);
        }

        [Fact]
        public void Remove_WhenItemNotInCollection_MustFail()
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };
            int elementToRemove = 6;
            int initialCount = 5;

            var remove = coll.Remove(elementToRemove);

            Assert.False(remove);
            Assert.Equal(coll.Count, initialCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void RemoveAt_WhenIndexInRange_MustSucceed(int index)
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };
            int elementToRemove = coll[index];
            int initialCount = 5;

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

        [Fact]
        public void Clear_WhenUsed_MustSucceed()
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };

            coll.Clear();

            Assert.Empty(coll);
        }
    }
}
