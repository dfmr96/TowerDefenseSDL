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
            get => position;
            protected set => position = value;
        }

        

        public GameObject(Vector2 initPosition, string spriteDir, Vector2 spriteSize)
        {
            position = initPosition;
            sprite.root = Engine.LoadImage(spriteDir);
            sprite.size = spriteSize;
            
            Program.gameObjects.Add(this);
            
            Engine.Debug("GO creado");
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