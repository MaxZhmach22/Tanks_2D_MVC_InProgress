using UnityEngine;

namespace Rewards
{
    internal class CurrencyView : BaseView
    {
        [SerializeField] private CurrencySlotView _currencyBaseWall;
        [SerializeField] private CurrencySlotView _currencyBombs;
        [SerializeField] private CurrencySlotView _currencyGunUpgrade;
        [SerializeField] private CurrencySlotView _currencyHelmetUpgrade;
        [SerializeField] private CurrencySlotView _currencyIce;

        public void SetValue(int value, RewardType rewardType)
        {
            switch (rewardType)
            {
                case RewardType.BaseWall:
                    BaseWall += value;
                    break;
                case RewardType.Bombs:
                    Bombs += value;
                    break;
                case RewardType.GunUpgrade:
                    GunUpgrade += value;
                    break;
                case RewardType.HelmetUpgrade:
                    HelmetUpgrade += value;
                    break;
                case RewardType.Ice:
                    Ice += value;
                    break;
            }
            RefreshText();
        }

        private int BaseWall
        {
            get => PlayerPrefs.GetInt(RewardsNameManager.BaseWall, 0);
            set => PlayerPrefs.SetInt(RewardsNameManager.BaseWall, value);
        }

        private int GunUpgrade
        {
            get => PlayerPrefs.GetInt(RewardsNameManager.GunUpgrade, 0);
            set => PlayerPrefs.SetInt(RewardsNameManager.GunUpgrade, value);
        }
        private int Bombs
        {
            get => PlayerPrefs.GetInt(RewardsNameManager.Bombs, 0);
            set => PlayerPrefs.SetInt(RewardsNameManager.Bombs, value);
        }

        private int HelmetUpgrade
        {
            get => PlayerPrefs.GetInt(RewardsNameManager.HelmetUpgrade, 0);
            set => PlayerPrefs.SetInt(RewardsNameManager.HelmetUpgrade, value);
        }
        private int Ice
        {
            get => PlayerPrefs.GetInt(RewardsNameManager.Ice, 0);
            set => PlayerPrefs.SetInt(RewardsNameManager.Ice, value);
        }


        private void Start() =>
            RefreshText();


        public void RefreshText()
        {
            _currencyBaseWall.SetData(BaseWall);
            _currencyBombs.SetData(Bombs);
            _currencyGunUpgrade.SetData(GunUpgrade);
            _currencyHelmetUpgrade.SetData(HelmetUpgrade);
            _currencyIce.SetData(Ice);
        }
}
}