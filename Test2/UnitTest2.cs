using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using System;

namespace Test2
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            Enemy newEnemy = new Enemy(EnemyColor.Red);

            newEnemy.TakeDamage();

            Assert.AreNotEqual(newEnemy.Health, 5);
        }
    }
}
