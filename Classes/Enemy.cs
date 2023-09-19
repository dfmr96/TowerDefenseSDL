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

        public Enemy(Vector2 initPosition, string spriteDir, float speed, Vector2 direction)
            : base(initPosition, spriteDir, new Vector2(16, 16))
        {
            this.speed = speed;
            this.direction = direction;
            Program.enemies.Add(this);
        }

        public override void Update()
        {
            position += direction * Program.DeltaTime * speed;
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, position.x - sprite.size.x / 2, position.y - sprite.size.y / 2);
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
            for (int i = 0; i < Program.towers.Count; i++)
            {
                if (Program.towers[i].Target == this) Program.towers[i].UnTarget();
                Program.towers[i].RemoveEnemy(this);
            }

            Program.enemies.Remove(this);
            Program.gameObjects.Remove(this);
            Engine.Debug("Enemigo muerto");
        }
    }
}