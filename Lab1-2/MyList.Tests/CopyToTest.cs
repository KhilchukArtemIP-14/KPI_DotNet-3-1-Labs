using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;
using System.Linq;

namespace MyList.Tests
{
    public class CopyToTest
    {
        [Theory]
        [MemberData(nameof(GetValidCopyToData))]
        public void CopyTo_WhenArrayCanFit_MustSucceed(int[] targetArray, int startIndex, CustomList<int> coll)
        {
            int[] arraySnapshot = (int[])targetArray.Clone();

            coll.CopyTo(targetArray, startIndex);

            Assert.Equal(arraySnapshot.Take(startIndex), targetArray.Take(startIndex));
            Assert.Equal(coll, targetArray.Skip(startIndex).Take(coll.Count));
            Assert.Equal(arraySnapshot.Skip(startIndex + coll.Count), targetArray.Skip(startIndex + coll.Count));
        }

        [Theory]
        [MemberData(nameof(GetInvalidCopyToData))]
        public void CopyTo_WhenArrayCantFit_MustThrow(int[] targetArray, int startIndex, CustomList<int> coll)
        {
            Action copyToSmall = () => coll.CopyTo(targetArray, startIndex);

            var exception = Assert.Throws<Exception>(copyToSmall);
            Assert.Equal("Array doesn't have enough space", exception.Message);
        }

        [Fact]
        public void CopyTo_WhenArrayIsNull_MustThrow()
        {
            int[] arr = null;
            CustomList<int> coll = new CustomList<int>() { 1, 2, 3, 4, 5 };

            Action copyToNull = () => coll.CopyTo(arr, 0);

            var exception = Assert.Throws<Exception>(copyToNull);
            Assert.Equal("Array is null",exception.Message);
        }

        public static IEnumerable<object[]> GetValidCopyToData()
        {
            //returns array to copy data to, starting index, and collection itself
            yield return new object[] { new int[] { -1, -1, -1, -1, -1, }, 0, new CustomList<int>() { 1, 2, 3, 4, 5 } };
            yield return new object[] { new int[] { -1, -1, -1, -1, -1, }, 2, new CustomList<int>() { 3, 4, 5 } };
            yield return new object[] { new int[] { -1, -1, -1, -1, -1, }, 2, new CustomList<int>() };
            yield return new object[] { new int[] { -1, -1, -1, -1, -1, }, 2, new CustomList<int>() { 3} };
        }

        public static IEnumerable<object[]> GetInvalidCopyToData()
        {
            //returns array to copy data to, starting index, and collection itself
            yield return new object[] { new int[] { -1, -1, -1, -1,}, 0, new CustomList<int>() { 1, 2, 3, 4, 5 } };
            yield return new object[] { new int[] { -1, -1, -1, -1, -1 }, 5, new CustomList<int>() { 1, 2, 3, 4, 5 } };
            yield return new object[] { new int[] { -1, -1, -1, -1, -1 }, 4, new CustomList<int>() { 1, 2} };
        }
    }
}
