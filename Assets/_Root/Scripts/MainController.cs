using System;
using Tools;
using UnityEngine;
using Rewards;

namespace Tanks
{
    internal sealed class MainController : BaseController
    {
        private Transform _placeForUi;
        private PlayerProfile _playerProfile;

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private SettingMenuController _settingMenuController;
        private DailyRewardController _rewardsController;
        private MapSizeConfig _mapSizeConfig;

        public MainController(Transform placeForUi, PlayerProfile playerProfile, MapSizeConfig mapSizeConfig)
        {
            _placeForUi = placeForUi;
            _playerProfile = playerProfile;
            _mapSizeConfig = mapSizeConfig;
            OnChangeGameState(_playerProfile.CurrentGameState.Value);
            _playerProfile.CurrentGameState.SubscribeOnChange(OnChangeGameState);
        }

        protected override void OnDispose()
        {
            DisposeControllers();
            _playerProfile.CurrentGameState.UnSubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    DisposeControllers();
                    _mainMenuController = new MainMenuController(_placeForUi, _playerProfile);
                    break;
                case GameState.Game:
                    DisposeControllers();
                    _gameController = new GameController(_placeForUi, _playerProfile, _mapSizeConfig);
                    break;
                case GameState.Rewards:
                    DisposeControllers();
                    _rewardsController = new DailyRewardController(_placeForUi, _playerProfile);
                    break;
                case GameState.Settings:
                    DisposeControllers();
                    _settingMenuController = new SettingMenuController(_placeForUi, _playerProfile);
                    break;
                default:
                    DisposeControllers();
                    break;
            }
        }

        private void DisposeControllers()
        {
            _mainMenuController?.Dispose();
            _settingMenuController?.Dispose();
            _gameController?.Dispose();
            _rewardsController?.Dispose();
        }
    }
}