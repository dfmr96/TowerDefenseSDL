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
            Enemy newEnemy = new Enemy(new Vector2(10, 10), EnemyColor.Red, 5, 20, 5, 1);
            Enemy newEnemy2 = new Enemy(new Vector2(10, 10), EnemyColor.Cyan, 5, 20, 5, 1);
            Enemy newEnemy3 = new Enemy(new Vector2(10, 10), EnemyColor.Yellow, 5, 20, 5, 1);

            newEnemy.DestroyEnemy();
            newEnemy2.DestroyEnemy();
            newEnemy3.DestroyEnemy();

            Assert.AreEqual(GameManager.Instance.EnemiesRemaining, 48);
        }
    }
}
