using UnityEngine;

namespace Tanks
{
    internal class SettingMenuController : BaseViewController<SettingMenuView>
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/settingMenu");
        private PlayerProfile _playerProfile;
        private readonly SettingMenuView _view;
        private readonly IAnimationButtons _animationButtons;
        protected override ResourcePath ResourcePath => _resourcePath;

        public SettingMenuController(Transform placeForUi, PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            _view = LoadView(placeForUi);
            _view.Init(Save, Back);
            _animationButtons = _view;
        }

        private void Save()
        {
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.SAVESETTINGSBUTTON, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => Debug.Log("Save Settings"));
        }


        private void Back()
        {
            _animationButtons.ButtonsDictionary.TryGetValue(ButtonsNameManager.BACKBUTTON, out var button);
            _view.PlayAnimation(button, _animationButtons.Duration, _animationButtons.Strength, null, () => _playerProfile.CurrentGameState.Value = GameState.Start);
        }
    
    }
}