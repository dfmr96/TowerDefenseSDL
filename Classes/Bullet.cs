using System.Diagnostics;

namespace MyGame
{
    public class Bullet : GameObject
    {
        private Vector2 direction = new Vector2(-1, 0);
        private float speed = 25;
        private float colliderRadius = 3;

        public Bullet(Vector2 initPos, string spriteDir)
            : base(initPos, spriteDir, new Vector2(6, 6))
        {
            /*position = initPos;
            sprite.root = Engine.LoadImage(spriteDir);*/
            //sprite.size = new Vector2(6, 6);
        }

        public override void Update()
        {
            position += direction * Program.DeltaTime * speed;

            CheckCollision();
            
            
        }

        public void CheckCollision()
        {
            for (int i = 0; i < Program.enemies.Count; i++)
            {
                Enemy enemy = Program.enemies[i];
                if (Vector2.Distance(position, enemy.Position) <= colliderRadius + enemy.ColliderRadius)
                {
                    Program.gameObjects.Remove(this);
                    enemy.TakeDamage();
                }
            }
        }

        ~Bullet()
        {
            Engine.Debug("Bala destruida");
        }

    public override void Render()
        {
            Engine.Draw(sprite.root, position.x - sprite.size.x / 2, position.y - sprite.size.y / 2);
        }
    }
}