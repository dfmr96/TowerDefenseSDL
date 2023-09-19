using System.Diagnostics;

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
                Enemy newEnemy = new Enemy(new Vector2(-Program.TILE_SIZE, 18.5f * Program.TILE_SIZE), "assets/enemy.png", 15, new Vector2(1, 0));
                spawnRateCounter = 0;
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