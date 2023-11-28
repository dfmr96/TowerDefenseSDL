using MyGame.Classes;
using System.Collections.Generic;
using System;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Net;

namespace MyGame
{
    public enum EnemyColor
    {
        Red,
        Yellow,
        Cyan
    }

    public enum EnemyAnimations
    {
        left = 0,
        up = 1,
        right = 2,
        down = 3,
        explosion = 4
    }
    public class Enemy : GameObject
    {
        private List<Animation> animations = new List<Animation>(new Animation[5]);
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

        public Enemy(Vector2 tilePos, EnemyColor color, int jewelsRewards, int health, int speed, int damage)
        {
            base.transform.position = new Vector2(-GameManager.TILE_SIZE * tilePos.x, GameManager.TILE_SIZE * tilePos.y);
            direction = new Vector2(1, 0);
            this.castle = GameManager.Instance.castle;
            enemyColor = color;
            CreateAnimations();
            this.jewelsRewards = jewelsRewards;
            this.health = health;
            this.speed = speed;
            this.damage = damage;

            currentAnimation = animations[2];

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

        private String GetAnimName(EnemyAnimations animID)
        {
            switch (animID)
            {
                case EnemyAnimations.left:
                    return "left";
                case EnemyAnimations.up:
                    return "up";
                case EnemyAnimations.right:
                    return "right";
                case EnemyAnimations.down:
                    return "down";
                case EnemyAnimations.explosion:
                    return "explosion";
            }

            return null;
        }

        private void CreateAnimations()
        {
            for (int i = 0; i < animations.Count; i++)
            {
                EnemyAnimations currentAnim = (EnemyAnimations)i;
                int frames = (currentAnim != EnemyAnimations.explosion) ? 4 : 8; 
                IterateAnimation(frames, GetAnimName(currentAnim), i);
            }
        }

        private void IterateAnimation(int framesCount, string animationName, int animationID)
        {
            List<IntPtr> frames = new List<IntPtr>(new IntPtr[framesCount]);
            for (int i = 0; i < framesCount; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/enemyAnimations/{animationName}/{GetFrameRoot(enemyColor)}/{i}.png");
                frames[i] = frame;
            }
            animations[animationID] = new Animation(animationName, frames, 0.2f, true);
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
                currentAnimation = animations[(int)EnemyAnimations.up];
            }

            if (direction == new Vector2(-1, 0))
            {
                currentAnimation = animations[(int)EnemyAnimations.left];
            }

            if (direction == new Vector2(0, 1))
            {
                currentAnimation = animations[(int)EnemyAnimations.down];
            }

            if (direction == new Vector2(1, 0))
            {
                currentAnimation = animations[(int)EnemyAnimations.right];
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
        }

        private void DestroyEnemy()
        {
            if (GameManager.Instance == null) return;

            GameManager.Instance.EnemiesRemaining--;
            GameManager.Instance.CheckEnemies();
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