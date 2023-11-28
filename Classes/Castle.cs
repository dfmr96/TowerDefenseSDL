using MyGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame.Classes
{
    public class Castle : IDamageable, IScoreable
    {
        private float health = 100;
        public float Health => health;

        public void GrantPoints()
        {
            GameManager.Instance.IncreasePoints(health * 100);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            Engine.Debug($"Castillo dañado, {health}");

            if (health <= 0)
            {
                GameManager.Instance.EndMatch(GameState.Defeat);
            }
        }
    }
}
