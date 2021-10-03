using DG.Tweening;
using System;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tanks
{
    internal sealed class MainMenuView : BaseView, IAnimationButtons
    {
        private Dictionary<string, Button> _buttonsDictionary;

        [Header("Buttons")]
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonQuit;
        [SerializeField] private Button _buttonRewards;

        [field: SerializeField] public AnimationButtonType AnimationButtonType { get; private set; } = AnimationButtonType.ChangeRotation;
        [field: SerializeField] public Ease CurveEase { get; private set; } = Ease.Linear;
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Strength { get; private set; }

        public IReadOnlyDictionary<string, Button> ButtonsDictionary => _buttonsDictionary;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction quitApp, UnityAction dailyRewards)
        {
            _buttonsDictionary ??= new Dictionary<string, Button>();
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonQuit.onClick.AddListener(quitApp);
            _buttonRewards.onClick.AddListener(dailyRewards);
            AddButtonsToTheDictionary();
        }


        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonQuit.onClick.RemoveAllListeners();
            _buttonsDictionary.Clear();
        }
        private void AddButtonsToTheDictionary()
        {
            _buttonsDictionary.Add(ButtonsNameManager.STARTBUTTON, _buttonStart);
            _buttonsDictionary.Add(ButtonsNameManager.SETTINGSBUTTON, _buttonSettings);
            _buttonsDictionary.Add(ButtonsNameManager.QUITAPPBUTTON, _buttonQuit);
            _buttonsDictionary.Add(ButtonsNameManager.DAILYREWARDS, _buttonRewards);
        }

        public void PlayAnimation(Button button, float duration, float strength, Action onStart = null, Action onFinish = null)
        {
            onStart?.Invoke();
            var rectTransform = button.gameObject.GetComponent<RectTransform>();
            var sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOShakeRotation(duration, strength));
            sequence.OnComplete(
                () => onFinish?.Invoke());
        }
    }
}