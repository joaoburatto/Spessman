using Mirror;
using UnityEngine;

namespace Spessman.Tilemap
{
    /// <summary>
    /// Manages a tile and its changes
    ///
    /// FURNITURE
    ///     Every furniture is in this layer.
    ///     - Shouldn't exist without a TURF.
    ///
    /// WALL
    ///     All walls are in this layer.
    ///     - Shouldn't exist if there isn't a TURF in the tile
    /// 
    /// TURF
    ///     The "tile" itself, this is the base layer for it, where we put the floor object,
    ///     the girder, this can also be null when there's nothing in the tile.
    ///
    /// PIPE OBJECT
    ///     Should manage all pipping, disposals and atmos stuff.
    /// 
    /// </summary>
    public class Tile : NetworkBehaviour
    {
        public GameObject[] furniture;
        public GameObject wall;
        public GameObject plenum;
        
        // pipe object for later
        public PipeObject pipeObject;
        
        [Server]
        private void UpdateFurniture(GameObject furniture, int layer = 0)
        {
            if (this.furniture[layer] != null) Destroy(this.furniture[layer]);
            this.furniture[layer] = furniture;
        }

        [Server]
        private void UpdateWall(GameObject wall)
        {
            if (this.wall != null) Destroy(this.wall);
            this.wall = wall;
        }

        [Server]
        private void UpdatePlenum(GameObject plenum)
        {
            if (this.plenum != null) Destroy(this.plenum);
            this.plenum = plenum;
        }
    }
}