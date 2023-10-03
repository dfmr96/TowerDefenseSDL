using MyGame.Classes;
using System;
using System.Linq;
using Tao.Sdl;

namespace MyGame
{
    public class EnemyFactory
    {
        private float spawnRate =5f;
        private float spawnRateCounter = 0;

        public void Update()
        {
            spawnRateCounter += Program.DeltaTime;

            if (Engine.KeyPress(Engine.KEY_0) && spawnRateCounter >= 1 / spawnRate)
            {
                CreateWave(5, 3, 0);

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

        public void CreateWave(int red, int yellow, int cyan)
        {
            for (int i = 0; i < red; i++)
            {          
                Enemy newEnemy = new Enemy(EnemyColor.Red);
                newEnemy.transform.position.x = (-100 * i) - 50;
                spawnRateCounter = 0;

                Engine.Debug("Iteracion");
            }

            for (int i = 0; i < yellow; i++)
            {
                Enemy newEnemy = new Enemy(EnemyColor.Yellow);
                newEnemy.transform.position.x = (-150 * i) - 50;
            }

            for (int i = 0; i < cyan; i++)
            {
                Enemy newEnemy = new Enemy(EnemyColor.Cyan);
                newEnemy.transform.position.x = (-300 * i) - 50;
            }

        }
        public void SpawnEnemy()
        {
            Enemy newEnemy = new Enemy(EnemyColor.Red);

        }
        private int callback(int interval)
        {
            Enemy newEnemy = new Enemy(EnemyColor.Red);
            Engine.Debug("Enemigo Creado");
            return 0;
        }
    }
}