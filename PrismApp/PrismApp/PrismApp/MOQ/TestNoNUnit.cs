using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrismApp.Services;
using System;

namespace PrismApp.MOQ
{
    [TestClass]
    public class TestNoNUnit
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.AreEqual(true, false);
        }
    }
}
