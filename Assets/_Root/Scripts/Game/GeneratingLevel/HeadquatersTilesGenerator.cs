using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal sealed class HeadquatersTilesGenerator : BaseTileGenerator
    {
        private Tilemap _headquarters;
        private int[,] _map;
        private MapSizeConfig _mapSizeConfig;

        public HeadquatersTilesGenerator(LevelView view, MapSizeConfig mapSizeConfig, int[,] map)
        {
            Init(view,mapSizeConfig, map);
            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[0], SetHeadquatersPosition(HeadquatersCorner.LeftDownCorner,_map));
            GenerateTilesObjects(_headquarters, _mapSizeConfig.HeadquatersTiles[1], SetHeadquatersPosition(HeadquatersCorner.RightUpCorner, _map));
        }

        private void Init(LevelView view, MapSizeConfig mapSizeConfig, int[,] map)
        {
            _map = map;
            _mapSizeConfig = mapSizeConfig;
            _headquarters = view.Headquarters;
        }

        private Vector3Int SetHeadquatersPosition(HeadquatersCorner corner, int[,] map)
        {
            Vector3Int position;
            switch (corner)
            {
                case HeadquatersCorner.LeftUpCorner:
                    return position = new Vector3Int(1, map.GetUpperBound(1) - 2, 0);
                case HeadquatersCorner.LeftDownCorner:
                    return position = new Vector3Int(1, 1, 0);
                case HeadquatersCorner.RightUpCorner:
                    return position = new Vector3Int(map.GetUpperBound(0) - 2, map.GetUpperBound(1) - 2, 0);
                case HeadquatersCorner.RightDownCorner:
                    return position = new Vector3Int(map.GetUpperBound(0) - 2, map.GetUpperBound(0) - 2, 0);

            }
            return default;
        }
    }
}