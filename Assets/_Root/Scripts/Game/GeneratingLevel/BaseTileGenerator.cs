using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal abstract class BaseTileGenerator
    {
        protected virtual void GenerateTilesObjects(Tilemap tileMap, TileBase tileOjects, Vector3Int position)
        {
            tileMap.SetTile(position, tileOjects);
        }
    }
}