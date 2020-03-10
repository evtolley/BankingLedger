using NUnit.Framework;

namespace LedgerDomain.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMethod_Should_Return_42()
        {
            var ledger = new Ledger();
            Assert.AreEqual(ledger.TestMethod(), 42);
        }
    }
}