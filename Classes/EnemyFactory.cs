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
                Enemy newEnemy = new Enemy(new Vector2(-GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE),
                    "assets/enemy01.png", 150, new Vector2(1, 0), 5);
                spawnRateCounter = 0;
                Engine.Debug($"{GameManager.Instance.enemies.Count()}");
            }
            if (Engine.KeyPress(Engine.KEY_9) && spawnRateCounter >= 1 / spawnRate)
            {
                Enemy newEnemy = new Enemy(new Vector2(-GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE),
                    "assets/enemy02.png", 30, new Vector2(1, 0), 12);
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