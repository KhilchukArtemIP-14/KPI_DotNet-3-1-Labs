using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;

namespace MyList.Tests
{
    public class EventTests
    {
        [Fact]
        public void Cleared_WhenListCleared_MustInvoke()
        {
            var list = new CustomList<int>() { 1,2,3,4,5 };
            var wasInvoked = false;
            EventHandler onClear = (sender, item) => wasInvoked = true;
            list.Cleared += onClear;

            list.Clear();

            Assert.True(wasInvoked);
        }

        [Fact]
        public void ItemAdded_WhenAdded_MustInvoke()
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            int addedItem=-1;
            EventHandler<int> onAdded = (sender, item) => { wasInvoked = true; addedItem = item; };
            list.ItemAdded += onAdded;

            list.Add(6);

            Assert.True(wasInvoked);
            Assert.Equal(6,addedItem);
        }
        [Theory]
        [InlineData(0,0)]
        [InlineData(2,3)]
        [InlineData(5, 6)]
        public void ItemInserted_WhenInserted_MustInvoke( int index, int value)
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            int insertedItem = -1;
            EventHandler<int> onInseted = (sender, item) => { wasInvoked = true; insertedItem = item; };
            list.ItemInserted += onInseted;

            list.Insert(index,value);

            Assert.True(wasInvoked);
            Assert.Equal(value, insertedItem);
        }
        [Fact]
        public void ItemInserted_WhenInsertedInEmptyAt0_MustInvoke()
        {
            var list = new CustomList<int>();
            var wasInvoked = false;
            int insertedItem = -1;
            EventHandler<int> onInseted = (sender, item) => { wasInvoked = true; insertedItem = item; };
            list.ItemInserted += onInseted;

            list.Insert(0, 1);

            Assert.True(wasInvoked);
            Assert.Equal(1, insertedItem);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void ItemRemoved_WhenRemoved_MustInvoke(int value)
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            int removedItem = -1;
            EventHandler<int> onRemoved = (sender, item) => { wasInvoked = true; removedItem = item; };
            list.ItemRemoved += onRemoved;

            list.Remove(value);

            Assert.True(wasInvoked);
            Assert.Equal(value, removedItem);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void ItemRemoved_WhenRemovedAt_MustInvoke(int index)
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            int removedItem = -1;
            int itemAtIndex = list[index];
            EventHandler<int> onRemoved = (sender, item) => { wasInvoked = true; removedItem = item; };
            list.ItemRemoved += onRemoved;

            list.RemoveAt(index);

            Assert.True(wasInvoked);
            Assert.Equal(itemAtIndex, removedItem);
        }

        [Theory]
        [InlineData(0,2)]
        [InlineData(2,4)]
        [InlineData(3,6)]
        public void ItemSet_WhenSet_MustInvoke(int index, int value)
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            int indexToBeSet = -1;
            EventHandler<int> onSet = (sender, item) => { wasInvoked = true; indexToBeSet = index; };
            list.ItemSet += onSet;

            list[index] = value;

            Assert.True(wasInvoked);
            Assert.Equal(index, indexToBeSet);
        }
        /*[Theory]
        [InlineData(-1, 0)]
        [InlineData(6, 6)]
        public void ItemInserted_WhenInsertedOutside_MustNotInvoke(int index, int value)
        {
            var list = new CustomList<int>() { 1, 2, 3, 4, 5 };
            var wasInvoked = false;
            EventHandler<int> onInseted = (sender, item) => wasInvoked = true;
            list.ItemInserted += onInseted;

            list.Insert(index, value);

            Assert.False(wasInvoked);
        }*/

    }
}
