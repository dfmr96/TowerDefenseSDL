using System;
using System.ComponentModel.Design;
using Tao.Sdl;

namespace MyGame
{
    public abstract class GameObject
    {
        public Vector2 position;

        public Sprite sprite;

        public Vector2 Position
        {
            get => position;
            protected set => position = value;
        }

        

        public GameObject(Vector2 initPosition, string spriteDir)
        {
            position = initPosition;
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

        public virtual void Render()
        {
            Engine.Draw(sprite.root, position.x, position.y);
        }
    }
}