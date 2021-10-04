using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal sealed class BaseMapObjects
    {
        private int[,] _map;
        private bool _isEmpty;
        private MapSizeConfig _mapSizeConfig;
        private Tilemap _groundTileMap;
        private Tilemap _bordersTileMap;
        private MapSize _mapSize;
        private Camera _mainCamera;

        public BaseMapObjects(MapSizeConfig mapSizeConfig, DifficultType difficultType, Tilemap groundTileMap, Tilemap bordersTileMap, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _mapSizeConfig = mapSizeConfig;
            _groundTileMap = groundTileMap;
            _bordersTileMap = bordersTileMap;
            _map = GeneretaeMap(difficultType, _mapSizeConfig);
            RenderMap(_map, _groundTileMap, _bordersTileMap, _mapSize);
        }

        private int[,] GeneretaeMap(DifficultType difficultType, MapSizeConfig mapSizeConfig)
        {
            int[,] map;
            switch (difficultType)
            {
                case DifficultType.Easy:
                    map = GenerateArray(mapSizeConfig.EasyMapConfig.MapSize._width, mapSizeConfig.EasyMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.EasyMapConfig.MapSize;
                    _mainCamera.orthographicSize = 4.3f;
                    return map;
                case DifficultType.Medium:
                    map = GenerateArray(mapSizeConfig.MediumMapConfig.MapSize._width, mapSizeConfig.MediumMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.MediumMapConfig.MapSize;
                    _mainCamera.orthographicSize = 6.5f;
                    return map;
                case DifficultType.Hard:
                    map = GenerateArray(mapSizeConfig.HardMapConfig.MapSize._width, mapSizeConfig.HardMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.HardMapConfig.MapSize;
                    _mainCamera.orthographicSize = 8.5f;
                    return map;
                case DifficultType.Insane:
                    map = GenerateArray(mapSizeConfig.InsaneMapConfig.MapSize._width, mapSizeConfig.InsaneMapConfig.MapSize._height, _isEmpty);
                    _mapSize = mapSizeConfig.InsaneMapConfig.MapSize;
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
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (empty)
                        map[x, y] = 0;
                    else
                        map[x, y] = 1;
                }
            }
            return map;
        }

        private void RenderMap(int[,] map, Tilemap baseTilemap, Tilemap borderTilemap, MapSize mapSize)
        {
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (x == 0 || y == 0)
                    {
                        borderTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(_mapSizeConfig.BorderTiles));
                        borderTilemap.SetTile(new Vector3Int(map.GetUpperBound(0) - 1, y, 0), RandomTile(_mapSizeConfig.BorderTiles));
                        borderTilemap.SetTile(new Vector3Int(x, map.GetUpperBound(1) - 1, 0), RandomTile(_mapSizeConfig.BorderTiles));
                    }
                    else
                        baseTilemap.SetTile(new Vector3Int(x, y, 0), RandomTile(_mapSizeConfig.GroundTiles));
                }
            }
        }

        private TileBase RandomTile(List<TileBase> listOfTiles)
        {
            return listOfTiles[Random.Range(0, listOfTiles.Count)];
        }

        public int[,] GetMap() => _map;

        public MapSize GetMapSize() => _mapSize;

    }
}