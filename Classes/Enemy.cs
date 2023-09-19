using System.Diagnostics;

namespace MyGame
{
    public class Enemy : GameObject
    {
        private int health = 3;
        private Vector2 direction;
        private float speed = 0;

        public int Health
        {
            get => health;
            private set
            {
                health = value;

                if (health <= 0)
                {
                    health = 0;
                    Program.enemies.Remove(this);
                    Program.gameObjects.Remove(this);
                    Engine.Debug("Enemigo muerto");
                }
            }
    }
        
        public Enemy(Vector2 initPosition, string spriteDir, float speed, Vector2 direction) 
            : base(initPosition,spriteDir, new Vector2(16,16))
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
            Engine.Debug("Enemigo herido");
        }
    }
}