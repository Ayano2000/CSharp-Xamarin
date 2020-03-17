using System;
using NUnit.Framework;

namespace PrismApp.Tests
{
    [TestFixture]
    public class TestBase
    {
        [Test]
        public void Test1()
        {
            Assert.That(true, Is.False); //fails, expects false
        }
    }
}
