using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


// This was just to check if I have everything installed correctly.
// on my side it finds the test but does not execute it.
namespace PrismApp.MOQ
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Assert.That(true, Is.False);
        }
    }
}
