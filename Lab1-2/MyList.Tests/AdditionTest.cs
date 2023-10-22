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
        /*[Fact]
        public void Add_WhenAddedToNonEmpty_MustSucceed()
        {
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4 };
            int initCount = coll.Count;
            coll.Add(5);

            Assert.Equal(5,coll.Last());
            Assert.Equal(initCount + 1, coll.Count);
        }

        [Fact]
        public void Add_WhenAddedToEmpty_MustSucceed()
        {
            CustomList<int> coll = new CustomList<int>();

            coll.Add(1);

            Assert.Equal(1, coll.Last());
            Assert.Single(coll);
        }*/

        [Theory]
        [MemberData(nameof(GetAddValidTestData))]
        public void Add_WhenUsed_MustSucceed(List<int> toAdd, CustomList<int> coll)
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
        [MemberData(nameof(GetInsertValidTestData))]
        public void Insert_WhenIndexIsRight_MustSucceed(List<Tuple<int, int>> valueIndexes, CustomList<int> coll)
        {
            foreach(var valueIndex in valueIndexes)
            {
                int value = valueIndex.Item1;
                int index = valueIndex.Item2;
                int oldCount = coll.Count;
                int oldValueAtIndex = index < oldCount ? coll[index] : 0;

                coll.Insert(index, value);

                Assert.Equal(value, coll[index]);
                if (index < oldCount)
                {
                    Assert.Equal(oldValueAtIndex, coll[index + 1]);
                }
                Assert.Equal(oldCount + 1, coll.Count);
            }
        }

        public static IEnumerable<object[]> GetAddValidTestData()
        {
            //returns list of elements to add and initial CustomList
            yield return new object[] { new List<int> { 6 }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<int> { 6, 7, 8, 9 }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<int> { 1 }, new CustomList<int>()};
            yield return new object[] { new List<int> { 1, 2, 3 }, new CustomList<int>() };
        }

        public static IEnumerable<object[]> GetInsertValidTestData()
        {
            //returns list of elements to insert in format: Tuple< VALUE, INDEX > and initial CustomList
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(0,0), Tuple.Create(3, 3), Tuple.Create(6, 6), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(0, 0), Tuple.Create(0, 0), Tuple.Create(0, 0), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(3, 3), Tuple.Create(3, 3), Tuple.Create(3, 3), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(6, 5), Tuple.Create(7, 6), Tuple.Create(8, 7), }, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { new List<Tuple<int, int>> { Tuple.Create(1, 0), Tuple.Create(3, 1), Tuple.Create(2, 1), }, new CustomList<int>() };
        }
    }
}
