using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;
using System.Linq;

namespace MyList.Tests
{
    public class IndexerTest
    {
        [Theory]
        [MemberData(nameof(GetSetterValidData))]
        public void Set_WhenIndexIsCorrect_MustSucced(int index, int value, CustomList<int> coll)
        {
            coll[index] = value;

            int actualValue = coll.ElementAt(index);
            Assert.Equal(value, actualValue);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void Set_WhenIndexIsOutside_MustThrow(int index, CustomList<int> coll)
        {
            Action wrongSet = () => coll[index] = -1;
            
            var exception = Assert.Throws<IndexOutOfRangeException>(wrongSet);
            Assert.Equal("Index was out of range", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GetGetterValidData))]
        public void Get_WhenIndexIsCorrect_MustSucced(int index, int expectedValue, CustomList<int> coll)
        {
            int actual = coll[index];

            Assert.Equal(expectedValue, actual);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void Get_WhenIndexIsOutside_MustThrow(int index, CustomList<int> coll)
        {
            Action wrongSet = () => { int a = coll[index]; };

            var exception = Assert.Throws<IndexOutOfRangeException>(wrongSet);
            Assert.Equal("Index was out of range", exception.Message);
        }
        public static IEnumerable<object[]> GetSetterValidData()
        {
            //returns index, value and collection
            yield return new object[] { 0, 6, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 4, 6, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 2, 6, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
        public static IEnumerable<object[]> GetGetterValidData()
        {
            //returns index, expected value and collection
            yield return new object[] { 0, 1, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 4, 5, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 2, 3, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
        public static IEnumerable<object[]> GetInvalidData()
        {
            //returns index and collection
            yield return new object[] { -1, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 5, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
    }
}
