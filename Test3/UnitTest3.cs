using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using MyGame.Classes;
using System;

namespace Test3
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestEnemiesRemaining()
        {
            Enemy newEnemy = new Enemy(EnemyColor.Red);
            Enemy newEnemy2 = new Enemy(EnemyColor.Yellow);
            Enemy newEnemy3 = new Enemy(EnemyColor.Cyan);

            //newEnemy.DestroyEnemy();
            //newEnemy2.DestroyEnemy();
            //newEnemy3.DestroyEnemy();

            Assert.AreEqual(GameManager.Instance.EnemiesRemaining, 48);
        }
    }
}
