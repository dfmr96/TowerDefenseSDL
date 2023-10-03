using MyGame.Classes;
using System;
using Tao.Sdl;

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
                Update();
                Render();
                //Engine.Debug($"{fps}, {DeltaTime}");
            }
        }

        public static void Init()
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
            GetMouse();

            SceneManager.Instance.Update();
        }

        private static void Render()

        {
            Engine.Clear();
            SceneManager.Instance.Render();
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
                    if (SceneManager.Instance.gameState == GameState.GamePlay)
                    {
                        Tower newTower = new Tower(new Vector2(m_event.button.x, m_event.button.y), "assets/tower.png");
                    }

                }
                //else if (Sdl.SDL_BUTTON_RIGHT == m_event.button.button)
                //{
                //    Engine.Debug("'Right mouse button is down");
                //}
                //else if (Sdl.SDL_BUTTON_MIDDLE == m_event.button.button)
                //{
                //    Engine.Debug("Middle mouse button is down");
                //}
            }
        }
    }
}