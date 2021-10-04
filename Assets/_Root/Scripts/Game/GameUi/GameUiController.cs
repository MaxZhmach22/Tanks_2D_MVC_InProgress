using System;
using Tools;
using UnityEngine;

namespace Tanks
{
    internal sealed class GameUiController : BaseViewController<GameUiView>
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/gameUiView");

        private readonly Transform _placeForUi;
        private readonly MapSizeConfig _mapSizeConfig;
        private readonly Camera _mainCamera;
        private readonly PlayerProfile _playerProfile;

        private readonly MoveMethodsClass _moveMethods;
        private readonly TextMethodsClass _textMethods;

        public SubscriptionProperty<MoveDirection> MoveDirectionState;
        public SubscriptionProperty<bool> ButtonState;

        private GameUiView _gameUiView;
        protected override ResourcePath ResourcePath => _resourcePath;

        public GameUiController(
            Transform placeForUi,
            PlayerProfile playerProfile, 
            MapSizeConfig mapSizeConfig, 
            Camera mainCamera)
        {
            _placeForUi = placeForUi;
            _playerProfile = playerProfile;
            _mapSizeConfig = mapSizeConfig;
            _mainCamera = mainCamera;
            MoveDirectionState = new SubscriptionProperty<MoveDirection>();
            ButtonState = new SubscriptionProperty<bool>();
            _moveMethods = new MoveMethodsClass();
            _textMethods = new TextMethodsClass();

            _gameUiView = GameUiViewInitialization(_placeForUi, _moveMethods, _textMethods);
        }
        private GameUiView GameUiViewInitialization(Transform placeForUi, MoveMethodsClass moveMethods, TextMethodsClass textMethods)
        {
            var gameUiView = LoadView(placeForUi);
                gameUiView.InitSystemButtons(_mainCamera, BackToMainMenu, MuteSounds, PauseGame, ResumeGame);
                //gameUiView.InitMoveControlsButtons(moveMethods.UpMove, moveMethods.DownMove,
                //                                   moveMethods.RigthMove, moveMethods.LeftMove);
                gameUiView.InitActionControlsButtons(ChoosePreviousBonus, ChooseNextBonus, ApplyBonus, Reload, Fire);
                gameUiView.InitGameStatsText(textMethods.ScoreTextSet(), textMethods.LifeTextSet(),
                                             textMethods.EnemyKilletTextSet(), textMethods.EnemiesLeftTextSet());
                gameUiView.InitBonusBulletsCountText(textMethods.BonusCountTextSet(), textMethods.BulletsCountTextSet());
                gameUiView.InitSubscriptionProperty(MoveDirectionState, ButtonState);
            
            return gameUiView;
        }


        private void BackToMainMenu() =>
            _playerProfile.CurrentGameState.Value = GameState.Start;

        private void MuteSounds() { }

        private void PauseGame() 
        {
            _gameUiView.PauseWindow.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log($"Paused {Time.timeScale}");
        }
        private void ResumeGame()
        {
            _gameUiView.PauseWindow.gameObject.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log($"Resumed {Time.timeScale}");
        }

        private void ChoosePreviousBonus() { }

        private void ChooseNextBonus() { }

        private void ApplyBonus() { }

        private void Reload() { }

        private void Fire() { }

        
    }
}