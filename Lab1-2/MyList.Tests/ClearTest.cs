using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyList;

namespace MyList.Tests
{
    public class ClearTest
    {
        [Fact]
        public void Clear_WhenListIsNotNull_MustSucceed()
        {
            var coll = new CustomList<int>() { 1, 2, 3, 4, 5 };

            coll.Clear();

            Assert.Empty(coll);
        }
    }
}
