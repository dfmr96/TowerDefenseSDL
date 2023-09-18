using System;

namespace MyGame
{
    public class Tower : GameObject
    {
        public float fireRate = 1;
        public float fireRateCounter = 0;
        public Vector2 Tile
        {
            get => new Vector2((int)Math.Floor(position.x / Program.TILE_SIZE),
                (int)Math.Floor(position.y / Program.TILE_SIZE));
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
            fireRateCounter += Program.DeltaTime;

            if (Engine.KeyPress(Engine.KEY_ESP) && fireRateCounter > 1 / fireRate)
            {
                CreateBullet();
                fireRateCounter = 0;
            }
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, position.x - Program.TILE_SIZE / 2, position.y - (Program.TILE_SIZE * 4 / 3));
        }

        public void CreateBullet()
        {
            Bullet newBullet = new Bullet(new Vector2(128, 128), "assets/bullet_01.png");
            Program.gameObjects.Add(newBullet);
            Engine.Debug($"Bala creada {newBullet.position.x} {newBullet.position.y}");
            //Engine.Debug($"{Program.gameObjects.Count}");
        }
    }
}