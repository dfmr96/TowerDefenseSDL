using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Tao.Sdl;

namespace MyGame
{
    class Program
    {
        static IntPtr image = Engine.LoadImage("assets/map.png");
        private static DateTime _startTime;
        private static float _lastTimeFrame;
        public static float DeltaTime;
        public const int ROWS = 40;
        public const int COLUMNS = 19;
        public const int TILE_SIZE = 32;
        public static List<GameObject> gameObjects = new List<GameObject>();

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

            Tower tower1 = new Tower(new Vector2(0, 0), "assets/tower.png");
            gameObjects.Add(tower1);
        }

        private static void Update()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }
        }

        private static void Render()

        {
            Engine.Clear();
            Engine.Draw(image, 0, 0);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Render();
            }

            Engine.Show();
        }
    }
}