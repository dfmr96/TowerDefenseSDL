using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Tao.Sdl;

namespace MyGame
{
    class Program
    {
        static IntPtr background = Engine.LoadImage("assets/map.png");
        private static DateTime _startTime;
        private static float _lastTimeFrame;
        public static float DeltaTime;
        public const int ROWS = 40;
        public const int COLUMNS = 19;
        public const int TILE_SIZE = 32;
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Tower> towers = new List<Tower>();
        public static EnemyFactory enemyFactory = new EnemyFactory();

        static void Main(string[] args)
        {
            Engine.Initialize();

            Initialize();

            while (true)
            {
                float currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
                DeltaTime = currentTime - _lastTimeFrame;
                _lastTimeFrame = currentTime;
                int fps = (int)Math.Floor(1 / DeltaTime);
                //Engine.Debug($"{fps}, {DeltaTime}");
                Update();
                Render();
            }
        }

        private static void Initialize()
        {
            _startTime = DateTime.Now;

            //Tower tower1 = new Tower(new Vector2(1f * TILE_SIZE, 18f * TILE_SIZE), "assets/tower.png");
        }

        private static void Update()
        {
            GetMouse();
            
            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                Engine.Debug($"{gameObjects.Count}");
            }

            enemyFactory.Update();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }

        private static void Render()

        {
            Engine.Clear();
            Engine.Draw(background, 0, 0);

            /*foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Render();
            }*/

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Render();
            }

            Engine.Show();
        }

        private static void GetMouse()
        {
            bool press = false;
            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event m_event;
            Sdl.SDL_PollEvent(out m_event);
            int x, y;

            if (Sdl.SDL_MOUSEMOTION == m_event.type)
            {
                Sdl.SDL_GetMouseState(out x, out y);
                //Engine.Debug($"{x},{y}");
            }

            if (Sdl.SDL_MOUSEBUTTONDOWN == m_event.type)
            {
                if (Sdl.SDL_BUTTON_LEFT == m_event.button.button)
                {
                    Engine.Debug("Left mouse button is down");
                    Tower newTower = new Tower(new Vector2(m_event.button.x,m_event.button.y), "assets/tower.png");
                }
                else if (Sdl.SDL_BUTTON_RIGHT == m_event.button.button)
                {
                    Engine.Debug("'Right mouse button is down");
                }
                else if (Sdl.SDL_BUTTON_MIDDLE == m_event.button.button)
                {
                    Engine.Debug("Middle mouse button is down");
                }
            }
        }
    }
}