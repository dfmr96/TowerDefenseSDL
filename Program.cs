using MyGame.Classes;
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

        public static GameManager gameManager;

        static void Main(string[] args)
        {
            Engine.Initialize();
            _startTime = DateTime.Now;
            GameManager.Instance.InitBoard();
            while (true)
            {
                UpdateDeltaTime();
                int fps = (int)Math.Floor(1 / DeltaTime);
                //Engine.Debug($"{fps}, {DeltaTime}");
                Update();
                Render();
            }
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

            if (GameManager.Instance != null)
            {
                GameManager.Instance.Update();
            }
        }

        private static void Render()

        {
            Engine.Clear();
            Engine.Draw(background, 0, 0);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.Render();
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
                    if (GameManager.Instance != null)
                    {
                        Tower newTower = new Tower(new Vector2(m_event.button.x, m_event.button.y), "assets/tower.png");
                    }
                    //Tower newTower = new Tower(new Vector2(m_event.button.x, m_event.button.y), "assets/tower.png");
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