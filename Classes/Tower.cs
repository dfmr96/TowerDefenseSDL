using System;

namespace MyGame
{
    public class Tower : GameObject
    {
        private float fireRate = 1;
        private float fireRateCounter = 0;
        public Vector2 Tile
        {
            get => new Vector2((int)Math.Floor(position.x / Program.TILE_SIZE),
                (int)Math.Floor(position.y / Program.TILE_SIZE));
        }

        public Tower(Vector2 initPos, string spriteDir) 
            : base(initPos,spriteDir, new Vector2(32,64))
        {
            /*position = initPos;
            sprite.root = Engine.LoadImage(spriteDir);*/
//            sprite.size = new Vector2(32, 64);
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
            Engine.Draw(sprite.root, (float)Math.Floor(Position.x / Program.TILE_SIZE) * Program.TILE_SIZE, (float)Math.Floor(Position.y / Program.TILE_SIZE) * Program.TILE_SIZE - (sprite.size.y / 2));
        }

        private void CreateBullet()
        {
            Bullet newBullet = new Bullet(new Vector2(10 * Program.TILE_SIZE, 18.5f * Program.TILE_SIZE), "assets/bullet_01.png");
            Engine.Debug($"Bala creada {newBullet.Position.x} {newBullet.Position.y}");
            //Engine.Debug($"{Program.gameObjects.Count}");
        }
    }
}