using UnityEngine;

namespace Tanks
{
    internal sealed class GameController : BaseController
    {

        private readonly PlayerProfile _playerProfile;
        private readonly LevelController _levelController;

        public GameController(Transform placeForUi, PlayerProfile playerProfile, MapSizeConfig mapSizeConfig)
        {
            _levelController = new LevelController(playerProfile, mapSizeConfig);
            AddController(_levelController);
        }

        protected override void OnDispose()
        {
            _levelController?.Dispose();
        }

    }
}