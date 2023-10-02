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
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Tower> towers = new List<Tower>();
        public EnemyFactory enemyFactory = new EnemyFactory();

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void InitBoard()
        {
            board = new int[ROWS, COLUMNS];
            Engine.Debug("Board inicializado");
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
        }

        public void Render()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Render();
            }
        }
    }
}
