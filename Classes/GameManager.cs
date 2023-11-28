using System;
using System.Collections.Generic;

namespace MyGame.Classes
{
    public class GameManager
    {
        private static GameManager instance;
        public const int ROWS = 24;
        public const int COLUMNS = 40;
        private int[,] board;
        private IntPtr background = Engine.LoadImage("assets/map.png");
        private Font healthFont = new Font("assets/Fonts/antiquity-print.ttf", 36);
        private IntPtr jewelUI = Engine.LoadImage("assets/jewel.png");
        private float jewels = 25;
        private float jewelPerSecond = 0.5f;
        private float jewelCounter = 0;
        private float enemiesRemaining = 51;
        public const int TILE_SIZE = 32;
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Tower> towers = new List<Tower>();
        public EnemyFactory enemyFactory = new EnemyFactory();
        public List<DirectionChanger> directionChangers = new List<DirectionChanger>();
        public Castle castle = new Castle();
        public static GameManager Instance
        {
            get
            {
                if (instance == null && SceneManager.Instance.GameState == GameState.MainMenu)
                {
                    instance = new GameManager();
                    Engine.Debug($"Game Manager init , {SceneManager.Instance.GameState}");
                }
                return instance;
            }
        }

        public float Jewels => jewels;

        public float EnemiesRemaining
        {
            get
            {
                return enemiesRemaining;
            }
            set => enemiesRemaining = value;
        }

        public void InitBoard()
        {
            Engine.OnMouseClick1 += CreateTower;
            board = new int[ROWS, COLUMNS]{
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
                                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
                            };
            PrintBoard();
            Engine.Debug("Board inicializado");
            CreateDirectionChangers();
            //enemyFactory.CreateWave(10, 0, 0);
            enemyFactory.CreateEnemyWave(EnemyType.Easy, 10);

        }

        public void PrintBoard()
        {
            string boardMatrix = "   ";
            for (int n = 0; n < ROWS; n++)
            {
                boardMatrix += " _";
            }

            boardMatrix += "\n";

            for (int i = 0; i < ROWS; i++)
            {
                if (i < 10) boardMatrix += " ";
                boardMatrix += $"{i}|";
                for (int j = 0; j < COLUMNS; j++)
                {
                    boardMatrix += $" {board[i, j]}";
                }
                boardMatrix += "\n";
            }

            Engine.Debug(boardMatrix);
        }

        private void CreateDirectionChangers()
        {
            directionChangers.Add(new DirectionChanger(new Vector2(37f * GameManager.TILE_SIZE, 18.5f * GameManager.TILE_SIZE), new Vector2(0, -1)));
            directionChangers.Add(new DirectionChanger(new Vector2(37f * GameManager.TILE_SIZE, 7.5f * GameManager.TILE_SIZE), new Vector2(-1, 0)));
            directionChangers.Add(new DirectionChanger(new Vector2(27.1f * GameManager.TILE_SIZE, 7.5f * GameManager.TILE_SIZE), new Vector2(0, 1)));
            directionChangers.Add(new DirectionChanger(new Vector2(27.5f * GameManager.TILE_SIZE, 15.5f * GameManager.TILE_SIZE), new Vector2(-1, 0)));
            directionChangers.Add(new DirectionChanger(new Vector2(6.5f * GameManager.TILE_SIZE, 15.5f * GameManager.TILE_SIZE), new Vector2(0, -1)));
            directionChangers.Add(new DirectionChanger(new Vector2(6.5f * GameManager.TILE_SIZE, 4.5f * GameManager.TILE_SIZE), new Vector2(0, 0)));
        }

        public void Update()
        {
            jewelCounter += Program.DeltaTime;

            if (jewelCounter > 1 / jewelPerSecond)
            {
                jewels++;
                jewelCounter = 0;
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                Engine.Debug($"{gameObjects.Count}");
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            for (int i = 0; i < directionChangers.Count; i++)
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
            Engine.DrawText($" = {EnemiesRemaining}", 1180, 48, 255, 255, 255, healthFont);
            Engine.Draw(jewelUI, 1168, 24, 64, 64);
            Engine.Draw(Engine.LoadImage("assets/enemy01.png"), 1168, 80, 64, 64);
        }

        public void GameOver()
        {
            //ClearAllList();
            //DestroyGameManager();
            SceneManager.Instance.ChangeScene(GameState.Defeat);
            //SceneManager.Instance.GameState = GameState.Defeat;
        }
        public void Victory()
        {
            SceneManager.Instance.ChangeScene(GameState.Victory);
            //SceneManager.Instance.GameState = GameState.Victory;
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

        public void CheckEnemies()
        {
            switch (enemiesRemaining)
            {
                case 45:
                    //enemyFactory.CreateWave(5, 1, 0);
                    enemyFactory.CreateEnemyWave(EnemyType.Easy, 5, EnemyType.Medium, 1);
                    break;
                case 37:
                    //enemyFactory.CreateWave(5, 3, 0);
                    enemyFactory.CreateEnemyWave(EnemyType.Easy, 5, EnemyType.Medium, 3);
                    break;
                case 29:
                    //enemyFactory.CreateWave(9, 4, 1);
                    enemyFactory.CreateEnemyWave(EnemyType.Easy, 9, EnemyType.Medium, 4, EnemyType.Hard, 1);
                    break;
                case 17:
                    //enemyFactory.CreateWave(8, 3, 3);
                    enemyFactory.CreateEnemyWave(EnemyType.Easy, 8, EnemyType.Medium, 3, EnemyType.Hard, 3);
                    break;
                case 0:
                    Victory();
                    break;
            }
        }

        public void CreateTower()
        {
            if (Jewels >= Tower.Cost)
            {
                Vector2 tile = Utils.GetTile(Engine.mousePos);

                if (!CanPlaceTower(tile)) return;

                Tower newTower = new Tower(Engine.mousePos, "assets/tower.png");
                board[(int)tile.y, (int)tile.x] = 1;
                board[(int)tile.y - 1, (int)tile.x] = 1;
                board[(int)tile.y - 1, (int)tile.x + 1] = 1;
                board[(int)tile.y, (int)tile.x + 1] = 1;
                Engine.Debug($"Mouse en: {Engine.mousePos.x}, {(int)Engine.mousePos.y}");
                Engine.Debug($"Mouse en Tile: {tile.y},{tile.x}");
                PrintBoard();
            }
        }

        public bool CanPlaceTower(Vector2 tile)
        {
            if (board[(int)tile.y, (int)tile.x] == 1)
            {
                Engine.Debug("No se puede colocar aca");
                return false;
            }
            else if ((int)tile.y - 2 < 0)
            {
                Engine.Debug("No se puede colocar, la parte superior de la torre queda fuera del mapa");
                return false;
            }
            else if ((int)tile.x + 2 < 0)
            {
                Engine.Debug("No se puede colocar, la parte derecha de la torre queda fuera del mapa");
                return false;
            }
            else if (board[(int)tile.y - 1, (int)tile.x] == 1)
            {
                Engine.Debug("Torre en el Tile de arriba");
                return false; 
            }

            else if (board[(int)tile.y - 1, (int)tile.x + 1] == 1)
            {
                Engine.Debug("Torre en el Tile diagonal arriba a la derecha");
                return false; 
            }

            else if (board[(int)tile.y, (int)tile.x + 1] == 1)
            {
                Engine.Debug("Torre en el Tile de la derecha");
                return false; 
            }


            return true;
        }
    }
}