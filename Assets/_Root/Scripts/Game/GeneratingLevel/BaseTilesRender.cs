using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal sealed class BaseTilesRender : BaseRender
    {

        private MapSizeConfig _mapSizeConfig;
        private Tilemap _groundTileMap;
        private Tilemap _bordersTileMap;
        private MapSize _mapSize;
        private Camera _mainCamera;
        private int[,] _map;
        private bool _isEmpty;
        private int _oldSpawnTilesCount;
        Dictionary<Vector3Int, string> _positionsOfObjects;

        public int OldSpawnTilesCount { get => _oldSpawnTilesCount; } //TODO сделать словарь со списком колличества объектвов взависимости от размера карты

        public BaseTilesRender(MapSizeConfig mapSizeConfig, DifficultType difficultType, Tilemap groundTileMap, Tilemap bordersTileMap, Camera mainCamera, Dictionary<Vector3Int, string> positionsOfObjects)
        {
            _mainCamera = mainCamera;
            _mapSizeConfig = mapSizeConfig;
            _groundTileMap = groundTileMap;
            _bordersTileMap = bordersTileMap;
            _positionsOfObjects = positionsOfObjects;
            _map = GeneretaeMap(difficultType, _mapSizeConfig);
            RenderMap(_map, _groundTileMap, _bordersTileMap, _mapSizeConfig);
        }

        private int[,] GeneretaeMap(DifficultType difficultType, MapSizeConfig mapSizeConfig)
        {
            int[,] map;
            switch (difficultType)
            {
                case DifficultType.Easy:
                    map = GenerateArray(mapSizeConfig.EasyMapConfig.MapSize._width, mapSizeConfig.EasyMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.EasyMapConfig.MapSize;
                    _oldSpawnTilesCount = 1;
                    _mainCamera.orthographicSize = 4.3f;
                    
                    return map;
                case DifficultType.Medium:
                    map = GenerateArray(mapSizeConfig.MediumMapConfig.MapSize._width, mapSizeConfig.MediumMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.MediumMapConfig.MapSize;
                    _oldSpawnTilesCount = 2;
                    _mainCamera.orthographicSize = 6.5f;
                    return map;
                case DifficultType.Hard:
                    map = GenerateArray(mapSizeConfig.HardMapConfig.MapSize._width, mapSizeConfig.HardMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.HardMapConfig.MapSize;
                    _oldSpawnTilesCount = 3;
                    _mainCamera.orthographicSize = 8.5f;
                    return map;
                case DifficultType.Insane:
                    map = GenerateArray(mapSizeConfig.InsaneMapConfig.MapSize._width, mapSizeConfig.InsaneMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.InsaneMapConfig.MapSize;
                    _oldSpawnTilesCount = 4;
                    _mainCamera.orthographicSize = 10.6f;
                    return map;
                default:
                    break;
            }
            return null;
        }

        private int[,] GenerateArray(int width, int height, bool empty)
        {
            int[,] map = new int[width, height];
            for (int x = map.GetLowerBound(0); x <= map.GetUpperBound(0); x++)
            {
                for (int y = map.GetLowerBound(1); y <= map.GetUpperBound(1); y++)
                {
                    if (empty)
                        map[x, y] = 0;
                    else
                        map[x, y] = 1;
                }
            }
            return map;
        }

        private void RenderMap(int[,] map, Tilemap baseTilemap, Tilemap borderTilemap, MapSizeConfig mapSizeConfig)
        {
            Vector3Int tilePosition;
            for (int x = 0; x <= map.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= map.GetUpperBound(1); y++)
                {
                    if (x == 0 || x == map.GetUpperBound(0))
                    {
                        var randomBorderTile = mapSizeConfig.BordersTile;
                        borderTilemap.SetTile(tilePosition = new Vector3Int(x, y, 0), randomBorderTile);
                        _positionsOfObjects.Add(tilePosition, randomBorderTile.name);
                        continue;
                    }
                    if (y == 0 || y == map.GetUpperBound(1))
                    {
                        var randomBorderTile = mapSizeConfig.BordersTile;
                        borderTilemap.SetTile(tilePosition = new Vector3Int(x, y, 0), randomBorderTile);
                        _positionsOfObjects.Add(tilePosition, randomBorderTile.name);
                    }
                    else
                        baseTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(mapSizeConfig.GroundTiles));
                }
            }
        }

        public int[,] GetMap() => _map;

        public MapSize GetMapSize() => _mapSize;

    }
}