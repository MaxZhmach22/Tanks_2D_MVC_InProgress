using UnityEngine;

namespace Tanks
{
    internal sealed class MapGeneratingCotroller : BaseController
    {
        private BaseMapObjects _baseMapObjects;
        private HeadquatersTilesGenerator _headquatersObjects;

        public MapGeneratingCotroller(LevelView view, DifficultType difficultType, MapSizeConfig mapSizeConfig, Camera mainCamera)
        {
            _baseMapObjects = new BaseMapObjects(mapSizeConfig, difficultType, view.BaseTilemap, view.BorderTilemap, mainCamera);
            _headquatersObjects = new HeadquatersTilesGenerator(view, mapSizeConfig, _baseMapObjects.GetMap());
            SetViewPositionInCenter(_baseMapObjects,view);
        }

        private void SetViewPositionInCenter(BaseMapObjects baseMapObjects, LevelView view)
        {
            var mapSize = baseMapObjects.GetMapSize();
            view.gameObject.transform.position = new Vector3(-mapSize._width / 2, -mapSize._height / 2, 0);
        }


    }
}
