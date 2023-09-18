using System.Diagnostics;

namespace MyGame
{
    public class Bullet : GameObject
    {
        public Vector2 direction = new Vector2(1,0);
        public float speed = 25;
        public Bullet(Vector2 initPos, string spriteDir)
        {
            position = initPos;
            sprite.root = Engine.LoadImage(spriteDir);
            sprite.size = new Vector2(6, 6);
            
            /*Engine.Debug($"Torre creada en [X: {Position.x}, Y: {Position.y}]");
            Engine.Debug($"{sprite.size.x} , {sprite.size.y}");
            Engine.Debug($"{Tile.x} , {Tile.y}");*/
        }

        public override void Update()
        {
            position += direction * Program.DeltaTime * speed;
            Engine.Debug($"{position}");
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, position.x - sprite.size.x / 2, position.y - sprite.size.y / 2);
        }
    }
}