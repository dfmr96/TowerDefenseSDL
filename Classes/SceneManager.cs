using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        private Font sceneDebugUI = new Font("assets/Fonts/antiquity-print.ttf", 24);
        private IntPtr defeatBG = Engine.LoadImage("assets/defeat.png");
        private IntPtr victoryBG = Engine.LoadImage("assets/victory.png");
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
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        GameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Defeat:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        GameState = GameState.MainMenu;
                    }
                    break;
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
                    break;
                case GameState.GamePlay:
                    GameManager.Instance.Render();
                    break;
                case GameState.Victory:
                    Engine.Draw(victoryBG, 0, 0);
                    break;
                case GameState.Defeat:
                    
                    Engine.Draw(defeatBG, 0, 0);
                    break;
            }
            Engine.DrawText($"{GameState}", 1200, 720, 255, 255, 255, sceneDebugUI);
        }

        public void ChangeScene(GameState gameState)
        {
            GameState = gameState;
        }
    }
}
