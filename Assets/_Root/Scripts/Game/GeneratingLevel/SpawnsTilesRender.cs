using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal class SpawnsTilesRender : BaseRender
    {
        private LevelView _view;
        private MapSizeConfig _mapSizeConfig;
        private int[,] _map;
        private int _oldSpawnTilesCount;
        private Dictionary<Vector3Int, string> _positionsOfTileObjects;
        private List<Vector3Int> _possiblePositionsList = new List<Vector3Int>();

        public SpawnsTilesRender(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, int oldSpawnTilesCount, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            Init(view, mapSizeConfig, map, oldSpawnTilesCount, positionsOfTileObjects);
            RenderSpawnTile(_view, _mapSizeConfig.HeadquatersTiles[0].name, _mapSizeConfig.PlayerSpawnTiles.name, _mapSizeConfig.PlayerSpawnTiles);
            RenderSpawnTile(_view, _mapSizeConfig.HeadquatersTiles[1].name, _mapSizeConfig.EnemiesSpawnTiles.name, _mapSizeConfig.EnemiesSpawnTiles);

        }

        private void RenderSpawnTile(LevelView view, string headquatersName, string nameOfTileToRender, TileBase tile)
        {
            FindFreePositions(_positionsOfTileObjects, _possiblePositionsList, headquatersName);
            var randomPosition = _possiblePositionsList[UnityEngine.Random.Range(0, _possiblePositionsList.Count - 1)];
            GenerateTilesObjects(view.Spawns, tile, randomPosition);
            _positionsOfTileObjects.Add(randomPosition, nameOfTileToRender);
            _possiblePositionsList.Clear();
        }

        private void FindFreePositions(Dictionary<Vector3Int, string> positionsOfTileObjects, List<Vector3Int> possiblePositionsList, string baseName)
        {
            Vector3Int possiblePossition;
            var collection = positionsOfTileObjects.Where(value => value.Value.Contains(baseName));
            var positionOfBase = collection.First().Key;
            for (int x = positionOfBase.x - 2; x <= positionOfBase.x + 2; x++) //TODO Вынести магические числа в переменные в зависимости от сложности карты
            {
                for (int y = positionOfBase.y - 2; y <= positionOfBase.y + 2; y++)
                {
                    if (x < _map.GetLowerBound(0) || x>_map.GetUpperBound(0)) //TODO ОТРЕФАКТОРИТЬ!
                        continue;
                    if (y < _map.GetLowerBound(1) || y > _map.GetUpperBound(1))
                        continue;
                    possiblePossition = new Vector3Int(x, y, 0);
                    var dim = positionOfBase - possiblePossition;
                    if (Mathf.Abs(dim.x) == 1 || Mathf.Abs(dim.y) == 1)
                        continue;
                    if (CheckIsPositionFree(possiblePossition, positionsOfTileObjects))
                        _possiblePositionsList.Add(possiblePossition);
                }
            }
        }

        private void Init(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, int oldSpawnTilesCount, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            _view = view;
            _mapSizeConfig = mapSizeConfig;
            _map = map;
            _oldSpawnTilesCount = oldSpawnTilesCount;
            _positionsOfTileObjects = positionsOfTileObjects;
            
        }


    }
}