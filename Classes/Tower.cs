using MyGame.Classes;
using MyGame.Interfaces;
using System;
using System.Collections.Generic;

namespace MyGame
{
    public class Tower : GameObject, IScoreable
    {
        private Animation idleAnimation;
        private Animation currentAnimation;
        private float fireRate = 1;
        private float fireRateCounter = 0;
        private Vector2 bulletSpawnPos;
        private float rangeRadius = 120;
        private List<Enemy> enemiesInRange = new List<Enemy>();
        private Enemy target;
        private static int cost = 9;

        public Enemy Target => target;
        public static int Cost => cost;
        public List<Enemy> EnemiesInRange => enemiesInRange;

        public Tower(Vector2 initPos, string spriteDir)
            : base(initPos, spriteDir, new Vector2(32, 64))
        {
            GameManager.Instance.IncreaseJewels(-cost);
            bulletSpawnPos = new Vector2(transform.position.x + sprite.size.x * 0.5f, transform.position.y - sprite.size.y * 0.5f);
            sprite.size = new Vector2(32, 64);
            CreateAnimations();
            currentAnimation = idleAnimation;
            GameManager.Instance.towers.Add(this);
            GrantPoints();
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
        }

        public override void Render()
        {
            Engine.Draw(currentAnimation.Frames, (float)Math.Floor(transform.position.x / GameManager.TILE_SIZE) * GameManager.TILE_SIZE,
                (float)Math.Floor(transform.position.y / GameManager.TILE_SIZE) * GameManager.TILE_SIZE - (sprite.size.y / 2));
        }

        private void CreateBullet()
        {
            Vector2 direction = Vector2.Normalize(target.SpriteCenter - bulletSpawnPos);

            Bullet tempBullet = new Bullet(bulletSpawnPos, "assets/bullet_01.png", direction);
            GameManager.Instance.gameObjects.Remove(tempBullet);
            Bullet newBullet = GameManager.Instance.bulletsPool.GetNewT(tempBullet);
            if (newBullet != null)
            {
                GameManager.Instance.gameObjects.Add(newBullet);

                newBullet.SetDirection(direction);
                newBullet.SetPosition(bulletSpawnPos);
                GameManager.Instance.bulletsPool.PrintBullets();
            }
        }

        private void CheckEnemiesAround()
        {
            if (GameManager.Instance.enemies.Count == 0) return;
            for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
            {
                if (Vector2.Distance(GameManager.Instance.enemies[i].SpriteCenter, bulletSpawnPos) < rangeRadius
                    && !enemiesInRange.Contains(GameManager.Instance.enemies[i]))
                {
                    enemiesInRange.Add(GameManager.Instance.enemies[i]);
                }

                if (Vector2.Distance(GameManager.Instance.enemies[i].SpriteCenter, bulletSpawnPos) > rangeRadius
                    && enemiesInRange.Contains(GameManager.Instance.enemies[i]))
                {
                    enemiesInRange.Remove(GameManager.Instance.enemies[i]);
                    if (GameManager.Instance.enemies[i] == target) UnTarget();
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

        public void GrantPoints()
        {
            GameManager.Instance.IncreasePoints(cost);
        }
    }
}