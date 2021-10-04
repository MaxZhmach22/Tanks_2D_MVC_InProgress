using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Tools;

namespace Tanks
{
    internal class GameUiView : BaseView
    {
        [Header("System Buttons")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _muteButton;
        [SerializeField] private Button _pauseButton;

        [Header("Move Controls Buttons")]
        [SerializeField] private LongClickedButton _upDirectionButton;
        [SerializeField] private LongClickedButton _downDirectionButton;
        [SerializeField] private LongClickedButton _rightDirectionButton;
        [SerializeField] private LongClickedButton _leftDirectionButton;

        [Header("Action Controls Buttons")]
        [SerializeField] private Button _leftChooseBonusButton;
        [SerializeField] private Button _rightChooseBonusButton;
        [SerializeField] private Button _applyBonusButton;
        [SerializeField] private Button _reloadButton;
        [SerializeField] private Button _fireButton;

        [Header("GameStats Text")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _lifeText;
        [SerializeField] private TMP_Text _enemyKilledText;
        [SerializeField] private TMP_Text _enemiesLeftText;

        [Header("Bullets & Bonus Count Text")]
        [SerializeField] private TMP_Text _bounsCountText;
        [SerializeField] private TMP_Text _bulletsCountText;

        [field: Header("Game State Windows")]
        [field: SerializeField] public Transform PauseWindow { get; private set; }
        [SerializeField] private Button _resumeButton;
        [field: SerializeField] public Transform GameoverWindow { get; private set; }
        [SerializeField] private Button _gameoverToMainMenuButton;

        private Camera _mainCamera;

        public void InitSystemButtons(Camera mainCamera,
                UnityAction backToMainMenu,
                UnityAction muteAllSound,
                UnityAction pauseGame,
                UnityAction resumeButton)

        {
            _mainCamera = mainCamera;
            _mainMenuButton.onClick.AddListener(backToMainMenu);
            _muteButton.onClick.AddListener(muteAllSound);
            _pauseButton.onClick.AddListener(pauseGame);
            _resumeButton.onClick.AddListener(resumeButton);
            _gameoverToMainMenuButton.onClick.AddListener(backToMainMenu);
        }
        //public void InitMoveControlsButtons(
        //        UnityAction upDirectionMove,
        //        UnityAction downDirectionMove,
        //        UnityAction rightDirectionMove,
        //        UnityAction leftDirectionMove)
        //{
        //    _upDirectionButton.onClick.AddListener(upDirectionMove);
        //    _downDirectionButton.onClick.AddListener(downDirectionMove);
        //    _rightDirectionButton.onClick.AddListener(rightDirectionMove);
        //    _leftDirectionButton.onClick.AddListener(leftDirectionMove);

        //}

        public void InitActionControlsButtons(
                UnityAction leftChooseBonus,
                UnityAction rightChooseBonus,
                UnityAction applyBonus,
                UnityAction reload,
                UnityAction fire)
        {
            _leftChooseBonusButton.onClick.AddListener(leftChooseBonus);
            _rightChooseBonusButton.onClick.AddListener(rightChooseBonus);
            _applyBonusButton.onClick.AddListener(applyBonus);
            _reloadButton.onClick.AddListener(reload);
            _fireButton.onClick.AddListener(fire);
        }


        public void InitGameStatsText(
               string scoreTextSet,
               string lifeTextSet,
               string enemyKilledTextSet,
               string enemiesLeftTextSet
            )
        {
            _scoreText.text = scoreTextSet;
            _lifeText.text = lifeTextSet;
            _enemyKilledText.text = enemyKilledTextSet;
            _enemiesLeftText.text = enemiesLeftTextSet;
        }

        public void InitBonusBulletsCountText(
             string bonusCountTextSet,
             string bulletsCountTextSet

           )
        {
            _bounsCountText.text = bonusCountTextSet;
            _bulletsCountText.text = bulletsCountTextSet;
        }

        public void InitSubscriptionProperty(SubscriptionProperty<MoveDirection> moveDirection, SubscriptionProperty<bool> longButtonState)
        {
            _upDirectionButton.Init(moveDirection, longButtonState);
            _downDirectionButton.Init(moveDirection, longButtonState);
            _rightDirectionButton.Init(moveDirection, longButtonState);
            _leftDirectionButton.Init(moveDirection, longButtonState);
        }
        private void OnDestroy()
        {
            _mainMenuButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _muteButton.onClick.RemoveAllListeners();
            _pauseButton.onClick.RemoveAllListeners();
            //_upDirectionButton.onClick.RemoveAllListeners();
            //_downDirectionButton.onClick.RemoveAllListeners();
            //_rightDirectionButton.onClick.RemoveAllListeners();
            //_leftDirectionButton.onClick.RemoveAllListeners();
            _leftChooseBonusButton.onClick.RemoveAllListeners();
            _rightChooseBonusButton.onClick.RemoveAllListeners();
            _applyBonusButton.onClick.RemoveAllListeners();
            _reloadButton.onClick.RemoveAllListeners();
            _fireButton.onClick.RemoveAllListeners();
        }

    }
}
