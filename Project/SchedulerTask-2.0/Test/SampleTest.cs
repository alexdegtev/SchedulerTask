using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class SampleTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expected = 0;
            int actual = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
