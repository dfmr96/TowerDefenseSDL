using MyGame.Classes;
using System.Diagnostics;

namespace MyGame
{
    public class Bullet : GameObject
    {
        private Vector2 direction = new Vector2(-1, 0);
        private float speed = 350;
        private float colliderRadius = 5;

        public Bullet(Vector2 initPos, string spriteDir, Vector2 direction)
            : base(initPos, spriteDir, new Vector2(6, 6))
        {
            this.direction = direction;
        }

        public override void Update()
        {
            transform.position += direction * Program.DeltaTime * speed;

            CheckCollision();
        }

        public void CheckCollision()
        {
            for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
            {
                Enemy enemy = GameManager.Instance.enemies[i];
                if (Vector2.Distance(transform.position, enemy.transform.position) <= colliderRadius + enemy.ColliderRadius)
                {
                    enemy.TakeDamage();
                    //GameManager.Instance.gameObjects.Remove(this);
                    for(int j = 0; j < GameManager.Instance.bulletsPool.activeList.Count; j++)
                    {
                        GameManager.Instance.bulletsPool.RecycleT(GameManager.Instance.bulletsPool.activeList[j]);
                    }
                }
            }
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
        }
    }
}