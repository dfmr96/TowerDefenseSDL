using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame.Classes;
using System;

namespace Test4
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestChangeScene()
        {
            SceneManager.Instance.ChangeScene(GameState.Defeat);

            Assert.AreEqual(SceneManager.Instance.GameState, GameState.Defeat);
        }
    }
}
