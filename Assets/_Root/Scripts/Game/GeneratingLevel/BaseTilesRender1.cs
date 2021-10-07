using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal abstract class BaseRender
    {
        protected Vector3Int SetTileRandomPosition(TileBase tile, Dictionary<Vector3Int, string> positionsOfTileObjects, int[,] map, string nameOfCompareTile)
        {
            bool notOriginPosition = true;
            Vector3Int position;
            do
            {
                position = new Vector3Int(Random.Range(map.GetLowerBound(0), map.GetUpperBound(0)), Random.Range(map.GetLowerBound(1), map.GetUpperBound(1)), 0);
                notOriginPosition = CheckPositionByValues(positionsOfTileObjects, notOriginPosition, position);
            }
            while (notOriginPosition);
            positionsOfTileObjects.Add(position, tile.name);
            return position;
        }

        private bool CheckPositionByValues(Dictionary<Vector3Int, string> positionsOfTileObjects, bool notOriginPosition, Vector3Int position)
        {
            foreach (var tileName in positionsOfTileObjects.Values)
            {
                if (CheckPosition(position, positionsOfTileObjects, tileName))
                {
                    notOriginPosition = true;
                    break;
                }
                else
                    notOriginPosition = false;
            }

            return notOriginPosition;
        }

        protected bool CheckIsPositionFree(Vector3Int possiblePossition, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            foreach (var position in positionsOfTileObjects.Keys)
            {
                if (possiblePossition == position)
                    return false;
            }
            return true;
        }

        private bool CheckPosition(Vector3Int position, Dictionary<Vector3Int, string> positionsOfTileObjects, string nameOfCompareTile)
        {
            bool samePosition = false;
            var allTilesOfCurrentType = positionsOfTileObjects.Where(tiles => tiles.Value.Contains(nameOfCompareTile));
            foreach (var tile in allTilesOfCurrentType)
            {
                if (tile.Key == position)
                {
                    samePosition = true;
                    break;
                }
            }
            return samePosition;
        }

        protected virtual TileBase RandomTile(List<TileBase> listOfTiles)
        {
            return listOfTiles[Random.Range(0, listOfTiles.Count)];
        }

        protected virtual void GenerateTilesObjects(Tilemap tileMap, TileBase tileOjects, Vector3Int position)
        {
            tileMap.SetTile(position, tileOjects);
        }
    }
}