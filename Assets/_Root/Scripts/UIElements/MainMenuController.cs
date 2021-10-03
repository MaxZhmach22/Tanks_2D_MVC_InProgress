using DG.Tweening;
using System;
using UnityEngine;

namespace Tanks
{
    internal class MainMenuController : BaseViewController<MainMenuView>
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/mainMenu");
        private readonly MainMenuView _view;
        private readonly IAnimationButtons _animationButtons;
        private PlayerProfile _playerProfile;

        protected override ResourcePath ResourcePath => _resourcePath;

        public MainMenuController(Transform placeForUi, PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, QuitApp, DailyRewards);
            _animationButtons = _view;
        }

        private void StartGame()
        {
            Debug.Log("Start Game");
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.STARTBUTTON, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => _playerProfile.CurrentGameState.Value = GameState.Game);
        }
           
        private void Settings()
        {
            Debug.Log("Settings");
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.SETTINGSBUTTON, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => _playerProfile.CurrentGameState.Value = GameState.Settings);
        }

        private void DailyRewards()
        {
            Debug.Log("Rewards");
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.DAILYREWARDS, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => _playerProfile.CurrentGameState.Value = GameState.Rewards);
        }

        private void QuitApp()
        {
            Debug.Log("Quit");
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.QUITAPPBUTTON, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => Application.Quit());
        }


    }
}