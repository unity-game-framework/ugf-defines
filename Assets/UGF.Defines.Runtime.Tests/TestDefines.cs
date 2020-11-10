using NUnit.Framework;

namespace UGF.Defines.Runtime.Tests
{
    public class TestDefines
    {
        [Test]
        public void Test1()
        {
#if TEST1
            Assert.Pass();
#else
            Assert.Fail("'TEST1' has no defined.");
#endif
        }

        [Test]
        public void Test2()
        {
#if TEST2
            Assert.Pass();
#else
            Assert.Fail("'TEST2' has no defined.");
#endif
        }
    }
}
