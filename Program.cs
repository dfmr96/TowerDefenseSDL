﻿using System;
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

            Tower tower1 = new Tower(new Vector2(32, 64), "assets/tower.png");
        }

        private static void Update()
        {
            enemyFactory.Update();

            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                Engine.Debug($"{gameObjects.Count}");
            }
            /*foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }*/

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
        
    }
}