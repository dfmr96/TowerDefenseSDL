using MyGame.Classes;
using MyGame.Interfaces;
using System.Diagnostics;

namespace MyGame
{
    public class Bullet : GameObject, IMoveable
    {
        private Vector2 direction = new Vector2(-1, 0);
        private float speed = 350;
        private float colliderRadius = 5;
        private float damage = 1;
        public Bullet(Vector2 initPos, string spriteDir, Vector2 direction)
            : base(initPos, spriteDir, new Vector2(6, 6))
        {
            this.direction = direction;
        }

        public override void Update()
        {
            Move();

            CheckCollision();
        }

        public void CheckCollision()
        {
            for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
            {
                Enemy enemy = GameManager.Instance.enemies[i];
                if (Vector2.Distance(transform.position, enemy.transform.position) <= colliderRadius + enemy.ColliderRadius)
                {
                    enemy.TakeDamage(damage);
                    GameManager.Instance.gameObjects.Remove(this);
                }
            }
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