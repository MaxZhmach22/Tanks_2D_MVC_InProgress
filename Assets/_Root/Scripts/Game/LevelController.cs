using UnityEngine;

namespace Tanks
{
    internal class LevelController : BaseViewController<LevelView>
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Level");
        private LevelView _view;
        private readonly MapGeneratingCotroller _mapGeneratingController;

        protected override ResourcePath ResourcePath => _viewPath;

        public LevelController(PlayerProfile playerProfile, MapSizeConfig mapSizeConfig)
        {
            _view = LoadView();
            _mapGeneratingController = new MapGeneratingCotroller(_view, playerProfile.CurrentDifficultType.Value, mapSizeConfig);
            AddController(_mapGeneratingController);
        }

        protected override void OnDispose()
        {
            _mapGeneratingController?.Dispose();
        }

    }
}