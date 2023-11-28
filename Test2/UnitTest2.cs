using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using System;

namespace Test2
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestEnemyTakeDamage()
        {
            Enemy newEnemy = new Enemy(new Vector2(10, 10), EnemyColor.Red, 5, 20, 5, 1);

            newEnemy.TakeDamage(1);

            Assert.AreNotEqual(newEnemy.Health, 5);
        }
    }
}
