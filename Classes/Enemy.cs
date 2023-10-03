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
        private float damage = 5;
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
            enemyColor = color;
            base.transform.position = new Vector2(-GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE);
            direction = new Vector2(1, 0);
            this.castle = GameManager.Instance.castle;

            switch (enemyColor)
            {
                case EnemyColor.Red:
                    jewelsRewards = 3;
                    health = 5;
                    speed = 75;
                    base.sprite.root = Engine.LoadImage("assets/enemy01.png");
                    break;
                case EnemyColor.Yellow:
                    jewelsRewards = 5;
                    health = 24;
                    speed = 45;
                    base.sprite.root = Engine.LoadImage("assets/enemy02.png");
                    break;
                case EnemyColor.Cyan:
                    jewelsRewards = 10;
                    health = 48;
                    speed = 30;
                    base.sprite.root = Engine.LoadImage("assets/enemy03.png");
                    break;
            }
            CreateAnimations();
            currentAnimation = rightAnimation;
            GameManager.Instance.enemies.Add(this);
        }

        private void CreateAnimations()
        {
            List<IntPtr> frames = new List<IntPtr>();

            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/right/{i}.png");
                frames.Add(frame);
            }
            rightAnimation = new Animation("idle", frames, 0.2f, true);
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
            transform.position += direction * Program.DeltaTime * speed;


        }

        public override void Render()
        {
            Engine.Draw(sprite.root, transform.position.x - sprite.size.x / 2, transform.position.y - sprite.size.y / 2);
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