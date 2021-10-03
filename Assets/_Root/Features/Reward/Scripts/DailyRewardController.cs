using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Rewards
{
    internal class DailyRewardController : BaseViewController<DailyRewardMenuView>
    {
        private ResourcePath _resourcePath = new ResourcePath("Prefabs/Rewards/rewardMenu");
        private readonly DailyRewardMenuView _rewardMenuView;

        private List<ContainerSlotRewardView> _slots;
        private readonly PlayerProfile _playerProfile;
        private readonly IAnimationButtons _buttons;

        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;
        private bool _isClicked;
        protected override ResourcePath ResourcePath => _resourcePath;

        public DailyRewardController(Transform placeForUi, PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            _rewardMenuView = LoadView(placeForUi);
            _rewardMenuView.Init();
            _buttons = _rewardMenuView;
            InitView();
            AddGameObject(_rewardMenuView.GetCurrencyView().gameObject);
        }

        protected override void OnDispose()
        {
            Deinit();
            base.OnDispose();
        }


        public void InitView()
        {
            if (_isInitialized)
                return;

            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }

        public void Deinit()
        {
            if (!_isInitialized)
                return;

            DeinitSlots();
            StopRewardsUpdating();
            UnsubscribeButtons();

            _isInitialized = false;
        }


        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _rewardMenuView.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate(_rewardMenuView.ContainerSlotRewardPrefab, _rewardMenuView.PlaceForRewardsWindows, false);

        private void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }


            private void StartRewardsUpdating() =>
            _coroutine = _rewardMenuView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _rewardMenuView.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _rewardMenuView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _rewardMenuView.TimeGetReward.Value;
            bool isDeadlineElapsed = timeFromLastRewardGetting.Seconds >= _rewardMenuView.TimeDeadline;
            bool isTimeToGetNewReward = timeFromLastRewardGetting.Seconds >= _rewardMenuView.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;
        }

        private void ResetRewardsState()
        {
            _rewardMenuView.TimeGetReward = null;
        }


        private void RefreshUi()
        {
            _buttons.ButtonsDictionary[ButtonsNameManager.GETREWARDBUTTON].interactable = _isGetReward;
            _rewardMenuView.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "Get Reward!";

            if (_rewardMenuView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _rewardMenuView.TimeGetReward.Value.AddSeconds(_rewardMenuView.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                string timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                       $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"{timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                Reward reward = _rewardMenuView.Rewards[i];
                int countDay = i + 1;
                bool isSelect = i == _rewardMenuView.CurrentSlotInActive;

                _slots[i].SetData(reward, countDay, isSelect);
            }
        }


        private void SubscribeButtons()
        {
            _buttons.ButtonsDictionary[ButtonsNameManager.GETREWARDBUTTON].onClick.AddListener(ClaimReward);
            _buttons.ButtonsDictionary[ButtonsNameManager.RESETTIMERBUTTON].onClick.AddListener(ResetTimer);
            _buttons.ButtonsDictionary[ButtonsNameManager.BACKBUTTON].onClick.AddListener(BackToMainMenu);
        }


        private void UnsubscribeButtons()
        {
            _buttons.ButtonsDictionary[ButtonsNameManager.GETREWARDBUTTON].onClick.RemoveListener(ClaimReward);
            _buttons.ButtonsDictionary[ButtonsNameManager.RESETTIMERBUTTON].onClick.RemoveListener(ResetTimer);
            _buttons.ButtonsDictionary[ButtonsNameManager.BACKBUTTON].onClick.RemoveListener(BackToMainMenu);
        }

        private void ClaimReward()
        {
            if (!_isGetReward || _isClicked)
                return;

            Reward reward = _rewardMenuView.Rewards[_rewardMenuView.CurrentSlotInActive];

            switch (reward.RewardType)
            {
                case RewardType.BaseWall:
                    _rewardMenuView.GetCurrencyView().SetValue(1, reward.RewardType);
                    break;
                case RewardType.Bombs:
                    _rewardMenuView.GetCurrencyView().SetValue(1, reward.RewardType);
                    break;
                case RewardType.GunUpgrade:
                    _rewardMenuView.GetCurrencyView().SetValue(1, reward.RewardType);
                    break;
                case RewardType.HelmetUpgrade:
                    _rewardMenuView.GetCurrencyView().SetValue(1, reward.RewardType);
                    break;
                case RewardType.Ice:
                    _rewardMenuView.GetCurrencyView().SetValue(1, reward.RewardType);
                    break;
            }

            _rewardMenuView.TimeGetReward = DateTime.UtcNow;
            _rewardMenuView.CurrentSlotInActive++;
            _rewardMenuView.PlayAnimation(GetButtonComponent(ButtonsNameManager.GETREWARDBUTTON),  
                                           _rewardMenuView.Duration, _rewardMenuView.Strength, 
                                           () => _isClicked = true, RefreshRewardsState);
            _isClicked = false;
            
        }

        private void BackToMainMenu()
        {
            if(!_isClicked)
            _rewardMenuView.PlayAnimation(GetButtonComponent(ButtonsNameManager.BACKBUTTON),
                                          _rewardMenuView.Duration,_rewardMenuView.Strength, () => _isClicked = true,
                                          () => _playerProfile.CurrentGameState.Value = GameState.Start);
        }

        private Button GetButtonComponent(string buttonName)
        {
            _buttons.ButtonsDictionary.TryGetValue(buttonName, out var button);
            return button;
        }

        private void ResetTimer()
        {
            if (_isClicked)
                return;
            _rewardMenuView.PlayAnimation(GetButtonComponent(ButtonsNameManager.RESETTIMERBUTTON),
                                         _rewardMenuView.Duration, _rewardMenuView.Strength, () => _isClicked = true,
                                         null);
            PlayerPrefs.DeleteAll();
            _rewardMenuView.GetCurrencyView().RefreshText();
            _isClicked = false;

        }
    }
}