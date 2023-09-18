using System;

namespace MyGame
{
    public class Tower : GameObject
    {
        public Vector2 Tile
        {
            get =>  new Vector2((int)Math.Floor(Position.x / Program.TILE_SIZE), (int)Math.Floor(Position.y / Program.TILE_SIZE));
            //private set => new Vector2()
        }
        public Tower(Vector2 initPos, string spriteDir)
        {
            position = initPos;
            sprite.root = Engine.LoadImage(spriteDir);
            sprite.size = new Vector2(32, 64);
            
            /*Engine.Debug($"Torre creada en [X: {Position.x}, Y: {Position.y}]");
            Engine.Debug($"{sprite.size.x} , {sprite.size.y}");
            Engine.Debug($"{Tile.x} , {Tile.y}");*/
        }

        public override void Update()
        {
            if (Engine.KeyPress(Engine.KEY_LEFT))
            {
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
            }
            
            if (Engine.KeyPress(Engine.KEY_UP))
            {
            }

            if (Engine.KeyPress(Engine.KEY_DOWN))
            {
            }

            if (Engine.KeyPress(Engine.KEY_ESC))
            {
            }

        }
    }
}