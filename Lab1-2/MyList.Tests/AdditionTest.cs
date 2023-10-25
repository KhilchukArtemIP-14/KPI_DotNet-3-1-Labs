using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;
using System.Linq;

namespace MyList.Tests
{
    public class AdditionTest
    {
        [Theory]
        [MemberData(nameof(GetAddValidTestData))]
        public void Add_WhenCollectionIsNotNull_MustSucceed(List<int> toAdd, CustomList<int> coll)
        {
            int initCount = coll.Count;
            foreach(var value in toAdd)
            {
                coll.Add(value);

                Assert.Equal(value, coll.Last());
            }

            Assert.Equal(initCount + toAdd.Count, coll.Count);
        }

        [Theory]
        [MemberData(nameof(GetInsertInRangeTestData))]
        public void Insert_WhenIndexIsInRangeOfList_MustSucceed(List<Tuple<int, int>> valueIndexes, CustomList<int> coll)
        {
            foreach(var valueIndex in valueIndexes)
            {
                int value = valueIndex.Item1;
                int index = valueIndex.Item2;
                int oldCount = coll.Count;
                int oldValueAtIndex = coll[index];

                coll.Insert(index, value);

                Assert.Equal(value, coll[index]);
                Assert.Equal(oldCount + 1, coll.Count);
                Assert.Equal(oldValueAtIndex, coll[index + 1]);
            }
        }

        [Theory]
        [MemberData(nameof(GetInsertOneAfterTestData))]
        public void Insert_WhenIndexIsOneAfterTheEndOfList_MustSucceed(List<Tuple<int, int>> valueIndexes, CustomList<int> coll)
        {
            foreach (var valueIndex in valueIndexes)
            {
                int value = valueIndex.Item1;
                int index = valueIndex.Item2;
                int oldCount = coll.Count;

                coll.Insert(index, value);

                Assert.Equal(value, coll[index]);
                Assert.Equal(oldCount + 1, coll.Count);
            }
        }

        [Theory]
        [MemberData(nameof(GetInsertInvalidTestData))]
        public void Insert_WhenIndexIsOutside_MustThrow(Tuple<int, int> valueIndex, CustomList<int> coll)
        {
            int value = valueIndex.Item1;
            int index = valueIndex.Item2;

            Action wrongInsert = () => coll.Insert(index, value);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(wrongInsert);
            Assert.Equal("Index out of range", exception.ParamName);
        }

        public static IEnumerable<object[]> GetAddValidTestData()
        {
            //returns list of elements to add and initial CustomList
            yield return new object[] { new List<int> { 6 }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<int> { 6, 7, 8, 9 }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<int> { 1 }, new CustomList<int>()};
            yield return new object[] { new List<int> { 1, 2, 3 }, new CustomList<int>() };
        }

        public static IEnumerable<object[]> GetInsertInRangeTestData()
        {
            //returns list of elements to insert in format: Tuple< VALUE, INDEX > and initial CustomList
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(3, 3), Tuple.Create(6, 6), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(0, 0), Tuple.Create(0, 0), Tuple.Create(0, 0), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(3, 3), Tuple.Create(3, 3), Tuple.Create(3, 3), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
        public static IEnumerable<object[]> GetInsertOneAfterTestData()
        {
            //returns list of elements to insert in format: Tuple< VALUE, INDEX > and initial CustomList
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(6, 5), Tuple.Create(7, 6), Tuple.Create(8, 7), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(1, 0), Tuple.Create(3, 1), Tuple.Create(2, 1), }, new CustomList<int>() };
        }
        public static IEnumerable<object[]> GetInsertInvalidTestData()
        {
            //returns element to insert in format: Tuple< VALUE, INDEX > and initial CustomList
            yield return new object[] { Tuple.Create(0, -1) , new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { Tuple.Create(0, 6) , new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { Tuple.Create(0, 1), new CustomList<int>() };
        }
    }
}
