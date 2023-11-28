using MyGame.Interfaces;

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

            if (health <= 0)
            {
                GameManager.Instance.EndMatch(GameState.Defeat);
            }
        }
    }
}
