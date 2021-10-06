using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal sealed class HeadquatersTilesGenerator : BaseTileGenerator
    {
        private Tilemap _headquarters;
        private int[,] _map;
        private MapSizeConfig _mapSizeConfig;
        private Vector3Int _headquaterPosition;
        Dictionary<Vector3Int, string> _positionsOfObjects;

        public HeadquatersTilesGenerator(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, Dictionary<Vector3Int, string> positionsOfObjects)
        {
            Init(view,mapSizeConfig, map, positionsOfObjects);
            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[0], SetHeadquatersPosition(HeadquatersCorner.LeftDownCorner,_map));
            _positionsOfObjects.Add(_headquaterPosition, _mapSizeConfig.HeadquatersTiles[0].name);
            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[1], SetHeadquatersPosition(HeadquatersCorner.RightUpCorner, _map));
            _positionsOfObjects.Add(_headquaterPosition, _mapSizeConfig.HeadquatersTiles[1].name);
        }

        private void Init(LevelView view, MapSizeConfig mapSizeConfig, int[,] map, Dictionary<Vector3Int, string> positionsOfObjects)
        {
            _map = map;
            _mapSizeConfig = mapSizeConfig;
            _headquarters = view.Headquarters;
            _positionsOfObjects = positionsOfObjects;
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

        
    }
}