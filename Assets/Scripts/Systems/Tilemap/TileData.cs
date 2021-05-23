using UnityEngine;

namespace Spessman.Tilemap
{
    
    public struct TileData
    {
        public Vector2 position;
        
        public string[] furniture;
        public string wall;
        public string plenum;

        public TileData(Vector2 position)
        {
            this.position = position;

            this.furniture = null;
            this.wall = null;
            this.plenum = null;
        }

        public void UpdateTileData(TileData data)
        {
            if (position != data.position) return;

            for (int i = 0; i < data.furniture.Length; i++)
            {
                if (furniture[i] != data.furniture[i])
                {
                    furniture[i] = data.furniture[i];
                }
            }

            if (wall != data.wall)
                wall = data.wall;
            if (plenum != data.plenum)
                plenum = data.plenum;
        }
    }
}