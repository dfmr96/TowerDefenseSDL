using MyGame.Classes;
using System.Diagnostics;

namespace MyGame
{
    public class Enemy : GameObject
    {
        private int health = 3;
        private Vector2 direction;
        private float speed = 0;
        private float colliderRadius = 8;
        public float ColliderRadius => colliderRadius;

        public int Health
        {
            get => health;
            private set { health = value; }
        }

        public Enemy(Vector2 initPosition, string spriteDir, float speed, Vector2 direction, int health)
            : base(initPosition, spriteDir, new Vector2(16, 16))
        {
            this.speed = speed;
            this.direction = direction;
            this.health = health;
            GameManager.Instance.enemies.Add(this);
        }

        public override void Update()
        {
            transform.position += direction * Program.DeltaTime * speed;
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
        }

        public void TakeDamage()
        {
            Health--;
            if (health <= 0)
            {
                health = 0;
                DestroyEnemy();
            }

            Engine.Debug("Enemigo herido");
        }

        private void DestroyEnemy()
        {
            for (int i = 0; i < GameManager.Instance.towers.Count; i++)
            {
                if (GameManager.Instance.towers[i].Target == this) GameManager.Instance.towers[i].UnTarget();
                GameManager.Instance.towers[i].RemoveEnemy(this);
            }

            GameManager.Instance.enemies.Remove(this);
            GameManager.Instance.gameObjects.Remove(this);
            Engine.Debug("Enemigo muerto");
        }
    }
}