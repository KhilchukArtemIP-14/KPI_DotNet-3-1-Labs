using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace MyList.Tests
{
    public class ContainsTest
    {
        [Theory]
        [MemberData(nameof(GetContainsTestData))]
        public void Contains_WhenCollectionIsNotNull_MustSucceed(int searchedValue, bool expectedResult, CustomList<int> coll)
        {
            var result = coll.Contains(searchedValue);

            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetContainsTestData()
        {
            //returns value to search index for, expected result and initial collection
            yield return new object[] { 1, true, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 3, true, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 5, true, new CustomList<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { 6, false, new CustomList<int> { 1, 2, 3, 4, 5 } };
        }
    }
}
