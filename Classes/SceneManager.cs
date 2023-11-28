using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public enum GameState
    {
        Logo,
        MainMenu,
        GamePlay,
        Victory,
        Defeat
    }
    public class SceneManager
    {
        private static SceneManager instance;
        private GameState gameState;
        private IntPtr mainMenuBG = Engine.LoadImage("assets/menu.png");
        private Font font = new Font("assets/Fonts/antiquity-print.ttf", 24);
        private IntPtr defeatBG = Engine.LoadImage("assets/defeat.png");
        private IntPtr victoryBG = Engine.LoadImage("assets/victory.png");
        private float blinkTime = 0.5f;
        private float timer = 0;
        private bool showPressEnter = false;
        private bool showPressSpace = false;
        private float points = 0;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                    instance.GameState = GameState.MainMenu;
                }
                return instance;
            }
        }

        public GameState GameState { get => gameState; private set => gameState = value; }

        public void Update()
        {
            switch (GameState)
            {
                case GameState.Logo:
                    break;
                case GameState.MainMenu:

                    CalculateBlinkTime(ref showPressEnter);

                    if (Engine.KeyPress(Engine.KEY_ENTER))
                    {
                        GameManager.Instance.DestroyGameManager();
                        GameManager.Instance.InitBoard();
                        GameState = GameState.GamePlay;
                    }
                    break;
                case GameState.GamePlay:
                    GameManager.Instance.Update();
                    break;
                case GameState.Victory:
                    CalculateBlinkTime(ref showPressSpace);
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        GameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Defeat:
                    CalculateBlinkTime(ref showPressSpace);
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        GameState = GameState.MainMenu;
                    }
                    break;
            }
        }

        private void CalculateBlinkTime(ref bool pressKey)
        {
            timer += Program.DeltaTime;

            if (timer > blinkTime)
            {
                //Engine.Debug($"{pressKey}");
                pressKey = !pressKey;
                timer = 0;
            }
        }

        public void Render()
        {
            switch (GameState)
            {
                case GameState.Logo:
                    break;
                case GameState.MainMenu:
                    Engine.Draw(mainMenuBG, 0, 0);
                    if (showPressEnter) Engine.DrawText($"PRESS ENTER", 550, 350, 0, 0, 0, font);
                    break;
                case GameState.GamePlay:
                    GameManager.Instance.Render();
                    break;
                case GameState.Victory:
                    Engine.Draw(victoryBG, 0, 0);
                    Engine.DrawText($"Points: {points}", 500, 300, 255, 255, 255, font);
                    if (showPressSpace) Engine.DrawText($"PRESS SPACE TO CONTINUE", 350, 700, 255, 255, 255, font);
                    break;
                case GameState.Defeat:
                    Engine.Draw(defeatBG, 0, 0);
                    Engine.DrawText($"Points: {points}", 500, 300, 255, 255, 255, font);
                    if (showPressSpace) Engine.DrawText($"PRESS SPACE TO CONTINUE", 350, 700, 255, 255, 255, font);
                    break;
            }
            Engine.DrawText($"{GameState}", 1200, 720, 255, 255, 255, font);
        }

        public void ChangeScene(GameState gameState)
        {
            GameState = gameState;
        }

        public void SetPoints(float points)
        {
            this.points = points;
        }
    }
}
