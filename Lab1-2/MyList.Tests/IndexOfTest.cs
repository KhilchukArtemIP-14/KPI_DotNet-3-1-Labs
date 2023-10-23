using System;
using Xunit;
using System.Collections.Generic;

namespace MyList.Tests
{
    public class IndexOfTest
    {
        [Theory]
        [MemberData(nameof(GetIndexOfData))]
        public void IndexOf_WhenUsed_MustSucceed(int searchedValue,int expectedIndex, CustomList<int> coll)
        {
            var index = coll.IndexOf(searchedValue);

            Assert.Equal(expectedIndex, index);
            if (expectedIndex == -1) Assert.DoesNotContain(searchedValue, coll);
        }

        public static IEnumerable<object[]> GetIndexOfData()
        {
            //returns value to search index for, expected index and initial collection
            yield return new object[] { 1, 0, new CustomList<int> { 1, 2, 3, 4, 5} };
            yield return new object[] { 3, 2, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 5, 4, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 6, -1, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
    }
}
