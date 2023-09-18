namespace MyGame
{
    public class Enemy : GameObject
    {
        private int health = 3;
        private Vector2 direction = Vector2.Zero;
        private float speed = 0;
        
        public Enemy(Vector2 initPosition, string spriteDir, float speed, Vector2 direction) 
            : base(initPosition,spriteDir, new Vector2(16,16))
        {
            this.speed = speed;
            this.direction = direction;
        }
    }
}