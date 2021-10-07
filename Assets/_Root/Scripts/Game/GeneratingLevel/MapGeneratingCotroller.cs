using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tanks
{
    internal sealed class MapGeneratingCotroller : BaseController
    {
        private BaseTilesRender _baseMapObjects;
        private HeadquatersTilesRender _headquatersObjects;
        private WallsTilesRender _wallsTilesRender;
        private SpawnsTilesRender _spawnsTilesRender;

        private Dictionary<Vector3Int, string> _positionsOfTileObjects;

        public MapGeneratingCotroller(LevelView view, DifficultType difficultType, MapSizeConfig mapSizeConfig, Camera mainCamera)
        {
            _positionsOfTileObjects = new Dictionary<Vector3Int, string>();
            _baseMapObjects = new BaseTilesRender(mapSizeConfig, difficultType, view.BaseTilemap, view.BorderTilemap, mainCamera, _positionsOfTileObjects); //TODO Разделить на 2 класса Генерацию карты и Рендера границ карты
            _headquatersObjects = new HeadquatersTilesRender(view, mapSizeConfig, _baseMapObjects.GetMap(), _baseMapObjects.OldSpawnTilesCount, _positionsOfTileObjects); //TODO Перенести ренед старых спаунов из этого класса в spawnTilesRender
            _spawnsTilesRender = new SpawnsTilesRender(view, mapSizeConfig, _baseMapObjects.GetMap(), _baseMapObjects.OldSpawnTilesCount, _positionsOfTileObjects);
            _wallsTilesRender = new WallsTilesRender(view, mapSizeConfig, _baseMapObjects.GetMap(), _positionsOfTileObjects);


            //_playerSpwanObjects = new PlayerSpawnGenerator(view, mapSizeConfig, ) 

            //var pos = _positionsOfTileObjects.Where(tiles => tiles.Value.Contains("Brick"));
            //foreach(var items in pos)
            //{
            //    Debug.Log(items.Key);
            //}

            SetViewPositionInCenter(_baseMapObjects,view);
        }

        private void SetViewPositionInCenter(BaseTilesRender baseMapObjects, LevelView view)
        {
            var mapSize = baseMapObjects.GetMapSize();
            view.gameObject.transform.position = new Vector3(-mapSize._width / 2, -mapSize._height / 2, 0);
        }
    }
}
