using MyGame.Classes;
using System;
using System.Linq;
using Tao.Sdl;

namespace MyGame
{
    public enum EnemyType
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
    public class EnemyFactory
    {

        public void CreateEnemyWave(EnemyType enemy1, int enemy1Quantity)
        {
            IterateEnemy(enemy1, enemy1Quantity, new Vector2(1, 18.5f));
        }

        public void CreateEnemyWave(EnemyType enemy1, int enemy1Quantity, EnemyType enemy2, int enemy2Quantity)
        {
            IterateEnemy(enemy1, enemy1Quantity, new Vector2(1, 18.5f));
            IterateEnemy(enemy2, enemy2Quantity, new Vector2(1, 18.5f));
        }
        public void CreateEnemyWave(EnemyType enemy1, int enemy1Quantity, EnemyType enemy2, int enemy2Quantity, EnemyType enemy3, int enemy3Quantity)
        {
            IterateEnemy(enemy1, enemy1Quantity, new Vector2(1, 18.5f));
            IterateEnemy(enemy2, enemy2Quantity, new Vector2(1, 18.5f));
            IterateEnemy(enemy3, enemy3Quantity, new Vector2(1, 18.5f));
        }

        private void IterateEnemy(EnemyType enemy1, int enemy1Quantity, Vector2 tilePos)
        {
            for (int i = 0; i < enemy1Quantity; i++)
            {
                Enemy newEnemy = CreateEnemy(enemy1, tilePos);
                SetEnemyOffset(enemy1, newEnemy, i);
            }
        }

        public void SetEnemyOffset(EnemyType enemyType, Enemy newEnemy, int posInQeue)
        {
            switch (enemyType)
            {
                case EnemyType.Easy:
                    newEnemy.transform.position.x = (-100 * posInQeue) - 50;
                    break;
                case EnemyType.Medium:
                    newEnemy.transform.position.x = (-150 * posInQeue) - 50;
                    break;
                case EnemyType.Hard:
                    newEnemy.transform.position.x = (-300 * posInQeue) - 50;
                    break;
            }
        }
        public Enemy CreateEnemy(EnemyType enemy, Vector2 tilePos)
        {
            switch (enemy)
            {
                case EnemyType.Easy:
                    return new Enemy(tilePos, EnemyColor.Red, 2, 5, 85, 5);

                case EnemyType.Medium:
                    return new Enemy(tilePos, EnemyColor.Yellow, 5, 24, 55, 10);

                case EnemyType.Hard:
                    return new Enemy(tilePos, EnemyColor.Cyan, 10, 48, 40, 15);
            }

            return null;
        }
    }
}