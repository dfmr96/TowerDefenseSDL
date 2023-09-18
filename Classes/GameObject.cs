using System;
using System.ComponentModel.Design;
using Tao.Sdl;

namespace MyGame
{
    public abstract class GameObject
    {
        protected Vector2 position;

        public Sprite sprite;

        public Vector2 Position
        {
            get => new Vector2 (position.x + (sprite.size.x / 2), position.y + (sprite.size.y * 3 / 4));
            protected set => position = new Vector2(value.x - (sprite.size.x / 2), value.y - (sprite.size.y * 3 / 4));
        }

        

        public GameObject(Vector2 initPosition, string spriteDir)
        {
            Position = initPosition;
            sprite.root = Engine.LoadImage(spriteDir);
            sprite.size = new Vector2(32, 64);
        }

        public GameObject()
        {
            ///
        }

        public virtual void Update()
        {
            ///
        }

        public void Render()
        {
            Engine.Draw(sprite.root, position.x, position.y);
        }
    }
}