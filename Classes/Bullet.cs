using MyGame.Classes;
using MyGame.Interfaces;

namespace MyGame
{
    public class Bullet : GameObject, IMoveable
    {
        private Vector2 direction = new Vector2(-1, 0);
        private float speed = 350;
        private float colliderRadius = 5;
        private float damage = 1;
        private float destroyCounter = 0;
        private float destroyTime = 2;
        public Bullet(Vector2 initPos, string spriteDir, Vector2 direction)
            : base(initPos, spriteDir, new Vector2(6, 6))
        {
            this.direction = direction;
        }

        public override void Update()
        {
            CheckLifeTime();
            Move();
            CheckCollision();
        }

        private void CheckLifeTime()
        {
            destroyCounter += Program.DeltaTime;
            if (destroyCounter >= destroyTime)
            {
                RemoveFromPool();
            }
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void CheckCollision()
        {
            for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
            {
                Enemy enemy = GameManager.Instance.enemies[i];
                if (Vector2.Distance(transform.position, enemy.transform.position) <= colliderRadius + enemy.ColliderRadius)
                {
                    enemy.TakeDamage(damage);
                    RemoveFromPool();
                }
            }
        }

        private void RemoveFromPool()
        {
            destroyCounter = 0;
            GameManager.Instance.gameObjects.Remove(this);
            GameManager.Instance.bulletsPool.RecycleT(this);
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
        }

        public void Move()
        {
            transform.position += direction * Program.DeltaTime * speed;
        }
    }
}