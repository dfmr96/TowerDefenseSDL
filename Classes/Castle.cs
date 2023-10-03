using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public class Castle
    {
        private float health = 5;

        public float Health => health;

        public void TakeDamage(float damage)
        {
            health -= damage;
            Engine.Debug($"Castillo dañado, {health}");

            if (health <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
