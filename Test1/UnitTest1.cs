using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using MyGame.Classes;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Test1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestJewelsSum()
        {
            GameManager.Instance.IncreaseJewels(5);

            Assert.AreEqual(GameManager.Instance.Jewels, 30);
        }
    }
}
