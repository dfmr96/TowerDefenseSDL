using MyGame.Classes;
using System.Diagnostics;
using System.Linq;

namespace MyGame
{
    public class EnemyFactory
    {
        private float spawnRate = 0.5f;
        private float spawnRateCounter = 0;

        public void Update()
        {
            spawnRateCounter += Program.DeltaTime;

            if (Engine.KeyPress(Engine.KEY_0) && spawnRateCounter >= 1 / spawnRate)
            {
                Enemy newEnemy = new Enemy(EnemyColor.Red);
                spawnRateCounter = 0;
                Engine.Debug($"{GameManager.Instance.enemies.Count()}");
            }
            if (Engine.KeyPress(Engine.KEY_9) && spawnRateCounter >= 1 / spawnRate)
            {
                Enemy newEnemy = new Enemy(EnemyColor.Yellow);
                spawnRateCounter = 0;
                Engine.Debug($"{GameManager.Instance.enemies.Count()}");
            }
        }

        public EnemyFactory()
        {
            spawnRate = 1;
            spawnRateCounter = 0;
            Engine.Debug("Factory creada");
        }
    }
}