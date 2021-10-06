using UnityEngine;
using Tools;

namespace Tanks
{
    internal sealed class GameController : BaseController
    {

        private readonly PlayerProfile _playerProfile;
        private readonly LevelController _levelController;
        private readonly Camera _mainCamera;
        private readonly PlayerController _playerController;
        private readonly GameUiController _gameUiController;


        public GameController(Transform placeForUi, PlayerProfile playerProfile, MapSizeConfig mapSizeConfig, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _playerProfile = playerProfile;
            _levelController = new LevelController(_playerProfile, mapSizeConfig, _mainCamera);
            _gameUiController = new GameUiController(placeForUi, _playerProfile, mapSizeConfig, _mainCamera);
            _playerController = new PlayerController(_levelController.GetPlaceForPlayerRespawn(), _playerProfile,
                                                    _gameUiController.MoveDirectionState,
                                                    _gameUiController.ButtonState);
            AddController(_levelController);
            AddController(_gameUiController);
            AddController(_playerController);
        }

        protected override void OnDispose()
        {
            _levelController?.Dispose();
            _playerController?.Dispose();
            _gameUiController?.Dispose();
        }

    }
}