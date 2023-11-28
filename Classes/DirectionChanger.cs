using MyGame.Core;

namespace MyGame.Classes
{
    public class DirectionChanger
    {
        public Transform transform = new Transform();
        private Vector2 directionToChange;
        private float detectionRadius = 12;

        public DirectionChanger(Vector2 position, Vector2 directionToChange)
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
                }
            }
        }

        public void Update()
        {
            CheckEnemies();
        }
    }
}
