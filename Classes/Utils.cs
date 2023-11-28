using MyGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public static class Utils
    {
        public static Vector2 GetTile(Vector2 pos)
        {
            Vector2 tile = new Vector2((int)Math.Floor(pos.x / GameManager.TILE_SIZE), (int)Math.Floor(pos.y / GameManager.TILE_SIZE));
            return tile;
        }
    }
}