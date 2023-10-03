using MyGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public class DirectionChanger
    {
        public Transform transform = new Transform();
        private Vector2 directionToChange;
        private float detectionRadius = 12;

        public DirectionChanger(Vector2 position,  Vector2 directionToChange)
        {
            transform.position = position;
            this.directionToChange = directionToChange;
            GameManager.Instance.directionChangers.Add(this);
        }

        public void CheckEnemies()
        {
            for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
            {
                Enemy enemy = GameManager.Instance.enemies[i];
                if (Vector2.Distance(transform.position, enemy.transform.position) <= detectionRadius)
                {
                    enemy.SetDirection(directionToChange);
                    //GameManager.Instance.gameObjects.Remove(this);
                    //Engine.Debug($"{transform.position.x},{transform.position.y}, {Vector2.Distance(transform.position, enemy.transform.position)}");
                }
            }
        }

        public void Update()
        {
            CheckEnemies();
        }
    }
}
