using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tanks
{
    internal sealed class MapGeneratingCotroller : BaseController
    {
        private BaseMapObjects _baseMapObjects;
        private HeadquatersTilesGenerator _headquatersObjects;

        private Dictionary<Vector3Int, string> _positionsOfTileObjects;

        public MapGeneratingCotroller(LevelView view, DifficultType difficultType, MapSizeConfig mapSizeConfig, Camera mainCamera)
        {
            _positionsOfTileObjects = new Dictionary<Vector3Int, string>();
            _baseMapObjects = new BaseMapObjects(mapSizeConfig, difficultType, view.BaseTilemap, view.BorderTilemap, mainCamera, _positionsOfTileObjects);
            _headquatersObjects = new HeadquatersTilesGenerator(view, mapSizeConfig, _baseMapObjects.GetMap(), _positionsOfTileObjects);
            //_playerSpwanObjects = new PlayerSpawnGenerator(view, mapSizeConfig, ) 

            var pos = _positionsOfTileObjects.Where(tiles => tiles.Value.Contains("Brick"));
            foreach(var items in pos)
            {
                Debug.Log(items.Key);
            }

            SetViewPositionInCenter(_baseMapObjects,view);
        }

        private void SetViewPositionInCenter(BaseMapObjects baseMapObjects, LevelView view)
        {
            var mapSize = baseMapObjects.GetMapSize();
            view.gameObject.transform.position = new Vector3(-mapSize._width / 2, -mapSize._height / 2, 0);
        }



    }
}
