using DG.Tweening;
using System;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tanks
{
    internal sealed class SettingMenuView : BaseView, IAnimationButtons
    {
        private Dictionary<string, Button> _buttonsDictionary;

        [Header("Buttons")]
        [SerializeField] private Button _buttonSave;
        [SerializeField] private Button _buttonBack;


        [field: SerializeField] public AnimationButtonType AnimationButtonType { get; private set; }
        [field: SerializeField] public Ease CurveEase { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Strength { get; private set; }

        public IReadOnlyDictionary<string, Button> ButtonsDictionary => _buttonsDictionary;

        public void Init(UnityAction startGame, UnityAction settings)
        {
            _buttonsDictionary ??= new Dictionary<string, Button>();
            _buttonSave.onClick.AddListener(startGame);
            _buttonBack.onClick.AddListener(settings);
            AddButtonsToTheDictionary();
        }

        public void OnDestroy()
        {
            _buttonSave.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
            _buttonsDictionary.Clear();
        }
        private void AddButtonsToTheDictionary()
        {
            _buttonsDictionary.Add(ButtonsNameManager.BACKBUTTON, _buttonBack);
            _buttonsDictionary.Add(ButtonsNameManager.SAVESETTINGSBUTTON, _buttonSave);
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