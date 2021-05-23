using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Spessman.Tilemap
{
    /// <summary>
    /// Controls the currently loaded Tilemap and the instantiated Tiles
    ///
    /// - Spawns tile data
    /// - Updates the tiles when their TileData is updated
    /// </summary>
    public class TilemapManager : NetworkBehaviour
    {
        public static TilemapManager singleton { get; private set; }
        
        // Currently selected tilemap
        public Tilemap tilemap;
        // Currently spawned tiles
        public Tile[][] tiles;

        private void Awake()
        {
            if (singleton != null) Destroy(gameObject);
            singleton = this;
        }

        [Server]
        public void UpdateTile(TileData data)
        {
            // updates the data
        }

        [ClientRpc]
        public void RpcUpdateTile(TileData data)
        {
            // tells the client the tile was updated
        }
    }
}