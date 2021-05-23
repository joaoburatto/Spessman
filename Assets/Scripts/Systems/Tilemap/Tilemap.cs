using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Spessman.Tilemap
{
    public class Tilemap : NetworkBehaviour
    {
        Vector2 size;
        public List<TileData> tileData;
        private TilemapManager tilemapManager;

        private void Awake()
        {
            tilemapManager = TilemapManager.singleton;
        }

        [Server]
        public TileData GetTileDataByPosition(Vector2 position) 
        {
            foreach (TileData tileData in tileData)
                if (tileData.position == position)
                    return tileData;

            return new TileData(position);
        }
        
        [Server]
        public TileData GetTileDataByPosition(int x, int y)
        {
            Vector2 position = new Vector2(x, y);
            foreach (TileData tileData in tileData)
                if (tileData.position == position)
                    return tileData;

            return new TileData(position);
        }

        [Server]
        public List<TileData> GetTileData()
        {
            List<TileData> tileData = new List<TileData>();
            
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    tileData.Add(GetTileDataByPosition(i, j));
                }
            }

            return tileData;
        }

        [Server]
        public void UpdateTileData(TileData data)
        {
            TileData tile = GetTileDataByPosition(data.position);
            tile.UpdateTileData(data);
            RpcUpdateTileData(data);
            // *beep
            tilemapManager.UpdateTile(data);
        }

        [ClientRpc]
        public void RpcUpdateTileData(TileData data)
        {
            TileData tile = GetTileDataByPosition(data.position);
            tile.UpdateTileData(data);
        }
    }
}