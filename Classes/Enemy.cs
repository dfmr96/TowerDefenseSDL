using MyGame.Classes;
using System.Collections.Generic;
using System;
using System.Net.Configuration;

namespace MyGame
{
    public enum EnemyColor
    {
        Red,
        Yellow,
        Cyan
    }
    public class Enemy : GameObject
    {
        private Animation rightAnimation;
        private Animation leftAnimation;
        private Animation upAnimation;
        private Animation downAnimation;
        private Animation explosionAnimation;
        private Animation currentAnimation;
        private EnemyColor enemyColor;
        private int health = 3;
        private float damage = 0;
        private Vector2 direction;
        private float speed = 0;
        private float colliderRadius = 8;
        private Castle castle;
        private float jewelsRewards = 3;
        public float ColliderRadius => colliderRadius;

        public int Health
        {
            get => health;
            private set { health = value; }
        }

        public Enemy(EnemyColor color)
        {
           // enemyColor = color;
            base.transform.position = new Vector2(-GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE);
            direction = new Vector2(1, 0);
            this.castle = GameManager.Instance.castle;

            switch (enemyColor)
            {
                case EnemyColor.Red:
                    CreateAnimations();
                    jewelsRewards = 2;
                    health = 5;
                    speed = 85;//85;
                    damage = 5;
                    //base.sprite.root = Engine.LoadImage("assets/enemy01.png");
                    currentAnimation = rightAnimation;
                    break;
                case EnemyColor.Yellow:
                    CreateAnimations();
                    jewelsRewards = 5;
                    health = 24;
                    speed = 55;
                    damage = 10;
                    //base.sprite.root = Engine.LoadImage("assets/enemy02.png");
                    currentAnimation = rightAnimation;
                    break;
                case EnemyColor.Cyan:
                    CreateAnimations();
                    jewelsRewards = 10;
                    health = 48;
                    speed = 40;
                    damage = 15;
                    //base.sprite.root = Engine.LoadImage("assets/enemy03.png");
                    currentAnimation = rightAnimation;
                    break;
            }


            GameManager.Instance.enemies.Add(this);
        }

        public Enemy(Vector2 tilePos, EnemyColor color, int jewelsRewards, int health, int speed, int damage)
        {
            // enemyColor = color;
            base.transform.position = new Vector2(-GameManager.TILE_SIZE * tilePos.x, GameManager.TILE_SIZE * tilePos.y);
            direction = new Vector2(1, 0);
            this.castle = GameManager.Instance.castle;

            enemyColor = color;
            CreateAnimations();
            this.jewelsRewards = jewelsRewards;
            this.health = health;
            this.speed = speed;
            this.damage = damage;

            currentAnimation = rightAnimation;

            GameManager.Instance.enemies.Add(this);
        }

        private String GetFrameRoot(EnemyColor color)
        {
            switch (color)
            {
                case EnemyColor.Red:
                    return "red";
                case EnemyColor.Yellow:
                    return "yellow";
                case EnemyColor.Cyan:
                    return "cyan";
                default:
                    return "";
            }
        }

        private void CreateAnimations()
        {
            List<IntPtr> rightFrames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/right/{GetFrameRoot(enemyColor)}/{i}.png");
                rightFrames.Add(frame);
            }
            rightAnimation = new Animation("right", rightFrames, 0.2f, true);

            List<IntPtr> upFrames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/up/{GetFrameRoot(enemyColor)}/{i}.png");
                upFrames.Add(frame);
            }
            upAnimation = new Animation("up", upFrames, 0.2f, true);

            List<IntPtr> leftFrames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/left/{GetFrameRoot(enemyColor)}/{i}.png");
                leftFrames.Add(frame);
            }
            leftAnimation = new Animation("left", leftFrames, 0.2f, true);

            List<IntPtr> downFrames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/down/{GetFrameRoot(enemyColor)}/{i}.png");
                downFrames.Add(frame);
            }
            downAnimation = new Animation("down", downFrames, 0.2f, true);

            List<IntPtr> explosionFrames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/explosion/{GetFrameRoot(enemyColor)}/{i}.png");
                explosionFrames.Add(frame);
            }
            explosionAnimation = new Animation("explosion", explosionFrames, 0.2f, false);
        }

        public void SetDirection(Vector2 directionToChange)
        {
            direction = directionToChange;

            if (directionToChange == new Vector2(0, 0))
            {
                castle.TakeDamage(damage);
                DestroyEnemy();
            }
        }

        public override void Update()
        {
            if (direction == new Vector2(0, -1))
            {
                currentAnimation = upAnimation;
            }

            if (direction == new Vector2(-1, 0))
            {
                currentAnimation = leftAnimation;
            }

            if (direction == new Vector2(0, 1))
            {
                currentAnimation = downAnimation;
            }

            if (direction == new Vector2(0, 0))
            {
                currentAnimation = explosionAnimation;
            }

            if (enemyColor == EnemyColor.Red) currentAnimation.Update();
            if (enemyColor == EnemyColor.Yellow) currentAnimation.Update();
            if (enemyColor == EnemyColor.Cyan) currentAnimation.Update();
            transform.position += direction * Program.DeltaTime * speed;

        }

        public override void Render()
        {
            Engine.Draw(sprite.root, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
            if (enemyColor == EnemyColor.Red) Engine.Draw(currentAnimation.Frames, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
            if (enemyColor == EnemyColor.Yellow) Engine.Draw(currentAnimation.Frames, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
            if (enemyColor == EnemyColor.Cyan) Engine.Draw(currentAnimation.Frames, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
        }

        public void TakeDamage()
        {
            Health--;
            if (health <= 0)
            {
                health = 0;
                GameManager.Instance.IncreaseJewels(jewelsRewards);
                DestroyEnemy();
            }

            Engine.Debug("Enemigo herido");
        }

        private void DestroyEnemy()
        {
            if (GameManager.Instance == null) return;

            GameManager.Instance.EnemiesRemaining--;
            GameManager.Instance.CheckEnemies();
            Engine.Debug($"{GameManager.Instance.EnemiesRemaining}");
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