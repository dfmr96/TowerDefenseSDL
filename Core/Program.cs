using MyGame.Classes;
using System;

namespace MyGame
{
    class Program
    {
        private static DateTime _startTime;
        private static float _lastTimeFrame;
        public static float DeltaTime;

        static void Main(string[] args)
        {
            Init();

            while (true)
            {
                UpdateDeltaTime();
                int fps = (int)Math.Floor(1 / DeltaTime);
                Engine.GetMouse();
                Update();
                Render();
                //Engine.Debug($"{fps}, {DeltaTime}");
            }
        }

        private static void Init()
        {
            Engine.Initialize();
            _startTime = DateTime.Now;
            SceneManager sceneManager = SceneManager.Instance;
        }

        private static void UpdateDeltaTime()
        {
            float currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
            DeltaTime = currentTime - _lastTimeFrame;
            _lastTimeFrame = currentTime;
        }

        private static void Update()
        {

            SceneManager.Instance.Update();
        }

        private static void Render()
        {
            Engine.Clear();
            SceneManager.Instance.Render();
            Engine.Show();
        }
    }
}