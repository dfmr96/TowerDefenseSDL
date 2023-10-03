using MyGame.Classes;
using MyGame.Core;
using System;
using System.ComponentModel.Design;
using Tao.Sdl;

namespace MyGame
{
    public abstract class GameObject
    {
        public Transform transform = new Transform();
        public Sprite sprite;


        public Vector2 SpriteCenter => new Vector2(transform.position.x + sprite.size.x / 2, transform.position.y - sprite.size.y / 2);

        public GameObject(Vector2 initPosition, string spriteDir, Vector2 spriteSize)
        {
            transform.position = initPosition;
            sprite.root = Engine.LoadImage(spriteDir);
            sprite.size = spriteSize;

            GameManager.Instance.gameObjects.Add(this);

            Engine.Debug("GO creado");
        }

        public GameObject()
        {
            GameManager.Instance.gameObjects.Add(this);
            Engine.Debug("GO creado");
        }

        public virtual void Update()
        {
            ///
        }

        public virtual void Render()
        {
            Engine.Draw(sprite.root, transform.position.x, transform.position.y);
        }
    }
}