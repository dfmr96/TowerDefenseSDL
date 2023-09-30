using MyGame.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyGame
{
    public class Tower : GameObject
    {
        private Animation idleAnimation;
        private Animation upgradeAnimation;
        private Animation currentAnimation;
        private float fireRate = 1;
        private float fireRateCounter = 0;
        private Vector2 bulletSpawnPos;
        private float rangeRadius = 120;
        private List<Enemy> enemiesInRange = new List<Enemy>();
        private Enemy target;
        public Enemy Target => target;
        public List<Enemy> EnemiesInRange => enemiesInRange;

        public Vector2 Tile
        {
            get => new Vector2((int)Math.Floor(position.x / Program.TILE_SIZE) * Program.TILE_SIZE,
                (int)Math.Floor(position.y / Program.TILE_SIZE) * Program.TILE_SIZE);
        }

        public Tower(Vector2 initPos, string spriteDir)
            : base(initPos, spriteDir, new Vector2(32, 64))
        {
            Program.towers.Add(this);
            bulletSpawnPos = new Vector2(Tile.x + sprite.size.x * 0.5f, Tile.y - sprite.size.y * 0.5f);
            /*position = initPos;
            sprite.root = Engine.LoadImage(spriteDir);*/
            //            sprite.size = new Vector2(32, 64);
            CreateAnimations();
            currentAnimation = idleAnimation;
        }

        private void CreateAnimations()
        {
            List<IntPtr> frames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/towerAnimations/idle/{i}.png");
                frames.Add(frame);
            }
            idleAnimation = new Animation("idle", frames, 0.2f, true);

            //for (int i = 0; i < 4; i++)
            //{
            //    IntPtr frame = Engine.LoadImage($"assets/towerAnimations/upgrade/{i}.png");
            //    frames.Add(frame);
            //}
            //upgradeAnimation = new Animation("upgrade", frames, 0.5f, true);
        }

        public override void Update()
        {
            currentAnimation.Update();
            CheckEnemiesAround();

            fireRateCounter += Program.DeltaTime;
            if (target != null && fireRateCounter > 1 / fireRate)
            {
                CreateBullet();
                fireRateCounter = 0;
            }

            /*if (Engine.KeyPress(Engine.KEY_0))
            {
                target = Program.enemies[0];
                CreateBullet();
            }
            */
            if (Engine.KeyPress(Engine.KEY_1))
            {
                Engine.Debug($"{enemiesInRange.Count}");
            }
        }

        public override void Render()
        {
            Engine.Draw(sprite.root, (float)Math.Floor(Position.x / Program.TILE_SIZE) * Program.TILE_SIZE,
                (float)Math.Floor(Position.y / Program.TILE_SIZE) * Program.TILE_SIZE - (sprite.size.y / 2));
            Engine.Draw(currentAnimation.Frames, (float)Math.Floor(Position.x / Program.TILE_SIZE) * Program.TILE_SIZE,
                (float)Math.Floor(Position.y / Program.TILE_SIZE) * Program.TILE_SIZE - (sprite.size.y / 2));
        }

        private void CreateBullet()
        {
            Vector2 direction = Vector2.Normalize(target.SpriteCenter - bulletSpawnPos);
            Bullet newBullet = new Bullet(bulletSpawnPos, "assets/bullet_01.png", direction);
            Engine.Debug($"Bala creada {newBullet.Position.x} {newBullet.Position.y}");
            //Engine.Debug($"{Program.gameObjects.Count}");
        }

        private void CheckEnemiesAround()
        {
            //if (Program.enemies.Count == 0) return;
            for (int i = 0; i < Program.enemies.Count; i++)
            {
                if (Vector2.Distance(Program.enemies[i].SpriteCenter, bulletSpawnPos) < rangeRadius
                    && !enemiesInRange.Contains(Program.enemies[i]))
                {
                    enemiesInRange.Add(Program.enemies[i]);
                    Engine.Debug($"Enemigo dentro de rango, total: {enemiesInRange}");
                }

                if (Vector2.Distance(Program.enemies[i].SpriteCenter, bulletSpawnPos) > rangeRadius
                    && enemiesInRange.Contains(Program.enemies[i]))
                {
                    enemiesInRange.Remove(Program.enemies[i]);
                    if (Program.enemies[i] == target) UnTarget();
                    Engine.Debug($"Enemigo fuera de rango, total {enemiesInRange}");
                }
            }

            if (enemiesInRange.Count > 0) target = enemiesInRange[0];
        }

        public void UnTarget()
        {
            target = null;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            if (enemiesInRange.Contains(enemy)) enemiesInRange.Remove(enemy);
        }
    }
}