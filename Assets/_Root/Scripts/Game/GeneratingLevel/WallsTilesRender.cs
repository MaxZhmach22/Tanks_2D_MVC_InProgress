using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    internal class WallsTilesRender
    {
        private LevelView view;
        private MapSizeConfig mapSizeConfig;
        private int[,] vs;
        private Dictionary<Vector3Int, string> positionsOfTileObjects;

        public WallsTilesRender(LevelView view, MapSizeConfig mapSizeConfig, int[,] vs, Dictionary<Vector3Int, string> positionsOfTileObjects)
        {
            this.view = view;
            this.mapSizeConfig = mapSizeConfig;
            this.vs = vs;
            this.positionsOfTileObjects = positionsOfTileObjects;
        }
    }
}