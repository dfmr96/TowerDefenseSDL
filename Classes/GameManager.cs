using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public  class GameManager
    {
        private static GameManager instance;
        public int[,] board;
        public const int ROWS = 40;
        public const int COLUMNS = 19;
        public const int TILE_SIZE = 32;
        private IntPtr background = Engine.LoadImage("assets/map.png");
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Tower> towers = new List<Tower>();
        public EnemyFactory enemyFactory = new EnemyFactory();
        public List<DirectionChanger> directionChangers = new List<DirectionChanger>();
        public Castle castle = new Castle();
        private Font healthFont = new Font("assets/Fonts/antiquity-print.ttf", 36);
        private IntPtr jewelUI = Engine.LoadImage("assets/jewel.png");
        private float jewels = 0;
        public static GameManager Instance
        {
            get
            {
                if (instance == null && SceneManager.Instance.gameState == GameState.MainMenu)
                {
                    instance = new GameManager();
                    Engine.Debug($"Game Manager init , {SceneManager.Instance.gameState}");
                }
                return instance;
            }
        }

        public void InitBoard()
        {
            board = new int[ROWS, COLUMNS];
            Engine.Debug("Board inicializado");
            directionChangers.Add(new DirectionChanger(new Vector2(37f * GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE), new Vector2(0,-1)));
            directionChangers.Add(new DirectionChanger(new Vector2(37f * GameManager.TILE_SIZE, 7.5f * GameManager.TILE_SIZE), new Vector2(-1, 0)));
            directionChangers.Add(new DirectionChanger(new Vector2(27.5f * GameManager.TILE_SIZE, 7.5f * GameManager.TILE_SIZE), new Vector2(0, 1)));
            directionChangers.Add(new DirectionChanger(new Vector2(27.5f * GameManager.TILE_SIZE, 15.5f * GameManager.TILE_SIZE), new Vector2(-1, 0)));
            directionChangers.Add(new DirectionChanger(new Vector2(6.5f * GameManager.TILE_SIZE, 15.5f * GameManager.TILE_SIZE), new Vector2(0, -1)));
            directionChangers.Add(new DirectionChanger(new Vector2(6.5f * GameManager.TILE_SIZE, 4.5f * GameManager.TILE_SIZE), new Vector2(0, 0)));
        }

        public void Update()
        {
            

            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                Engine.Debug($"{gameObjects.Count}");
            }

            enemyFactory.Update();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            for (int i = 0; i <directionChangers.Count; i++)
            {
                directionChangers[i].Update();
            }
        }

        public void Render()
        {
            Engine.Draw(background, 0, 0);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Render();
            }
            Engine.DrawText($"{castle.Health}/100", 142, -5, 255, 255, 255, healthFont);
            Engine.DrawText($" = {jewels}", 1180, 16, 255, 255, 255, healthFont);
            Engine.Draw(jewelUI, 1168, 24, 64,64);
        }

        public void GameOver()
        {
            //ClearAllList();
            //DestroyGameManager();
            SceneManager.Instance.gameState = GameState.Defeat;
        }

        public void DestroyGameManager()
        {
            instance = null;
        }

        private void ClearAllList()
        {
            gameObjects.Clear();
            enemies.Clear();
            towers.Clear();
            directionChangers.Clear();
        }

        public void IncreaseJewels(float amount)
        {
            jewels += amount;
        }
    }
}
