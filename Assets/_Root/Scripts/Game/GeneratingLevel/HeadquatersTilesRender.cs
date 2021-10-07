using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal sealed class HeadquatersTilesRender : BaseRender
    {
        private Tilemap _headquarters;
        private int[,] _map;
        private MapSizeConfig _mapSizeConfig;
        private Vector3Int _headquaterPosition;
        private int _oldSpawnTilesCount;
        Dictionary<Vector3Int, string> _positionsOfTileObjects;

        public HeadquatersTilesRender(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, int oldSpawnTilesCount, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            Init(view, mapSizeConfig, map, oldSpawnTilesCount, positionsOfTileObjects);
            InitHeadquaters();
            SetOldSpawnTiles(view, map);

        }

        private void InitHeadquaters()
        {

            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[0], SetHeadquatersPosition(HeadquatersCorner.LeftDownCorner, _map));
            _positionsOfTileObjects.Add(_headquaterPosition, _mapSizeConfig.HeadquatersTiles[0].name);
            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[1], SetHeadquatersPosition(HeadquatersCorner.RightUpCorner, _map));
            _positionsOfTileObjects.Add(_headquaterPosition, _mapSizeConfig.HeadquatersTiles[1].name);
        }

        private void Init(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, int oldSpawnTilesCount, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            _map = map;
            _mapSizeConfig = mapSizeConfig;
            _headquarters = view.Headquarters;
            _oldSpawnTilesCount = oldSpawnTilesCount;
            _positionsOfTileObjects = positionsOfTileObjects;
        }

        private Vector3Int SetHeadquatersPosition(HeadquatersCorner corner, int[,] map)
        {
            switch (corner)
            {
                case HeadquatersCorner.LeftUpCorner:
                    return _headquaterPosition = new Vector3Int(map.GetUpperBound(0) - 1, map.GetUpperBound(1) - 1, 0);
                case HeadquatersCorner.LeftDownCorner:
                    return _headquaterPosition = new Vector3Int(map.GetLowerBound(0) + 1, map.GetLowerBound(1) + 1, 0);
                case HeadquatersCorner.RightUpCorner:
                    return _headquaterPosition = new Vector3Int(map.GetUpperBound(0) - 1, map.GetUpperBound(1) - 1, 0);
                case HeadquatersCorner.RightDownCorner:
                    return _headquaterPosition = new Vector3Int(map.GetUpperBound(0) - 1, map.GetLowerBound(1) - 1, 0);
            }
            return default;
        }   
        private void SetOldSpawnTiles(LevelView view, int[,] map)
        {
            for (int i = 0; i < _oldSpawnTilesCount; i++)
            {
                GenerateTilesObjects(view.BaseTilemap, _mapSizeConfig.OldSpawnsTile,     
                                    SetTileRandomPosition(_mapSizeConfig.OldSpawnsTile, 
                                    _positionsOfTileObjects, map, _mapSizeConfig.HeadquatersTiles[0].name));
            }
        }
    }
}