using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Tanks;

namespace Rewards
{
    internal sealed class DailyRewardMenuView : BaseView, IAnimationButtons
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);
        private Dictionary<string, Button> _buttonsDictionary;
        private CurrencyView _currencyView;

        [Header("Buttons")]
        [SerializeField] private Button _buttonGetReward;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonResetTimer;

        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public float TimeCooldown { get; private set; } = 86400;
        [field: SerializeField] public float TimeDeadline { get; private set; } = 172800;

        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform PlaceForRewardsWindows { get; private set; }
        [field: SerializeField] public Transform PlacePlayerPrefRewards { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public CurrencyView CurrencyView { get; private set; }

        [field: Header("Buttons Animations")]
        [field: SerializeField] public AnimationButtonType AnimationButtonType { get; private set; }
        [field: SerializeField] public Ease CurveEase { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Strength { get; private set; }

        public IReadOnlyDictionary<string, Button> ButtonsDictionary => _buttonsDictionary;

        public void Init()
        {
            _buttonsDictionary ??= new Dictionary<string, Button>();
            AddButtonsToTheDictionary();
            _currencyView = LoadCurrencyView();
        }
        private void AddButtonsToTheDictionary()
        {
            _buttonsDictionary.Add(ButtonsNameManager.BACKBUTTON, _buttonBack);
            _buttonsDictionary.Add(ButtonsNameManager.GETREWARDBUTTON, _buttonGetReward);
            _buttonsDictionary.Add(ButtonsNameManager.RESETTIMERBUTTON, _buttonResetTimer);
        }

        private CurrencyView LoadCurrencyView()
        {
            CurrencyView prefab = Instantiate<CurrencyView>(CurrencyView, PlacePlayerPrefRewards, false);
            return (prefab);
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

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey, null);
                return !string.IsNullOrEmpty(data) ? (DateTime?)DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }
        public CurrencyView GetCurrencyView()
        {
            return _currencyView;
        }

        public void OnDestroy()
        {
            _buttonsDictionary.Clear();
            
        }
    }
}