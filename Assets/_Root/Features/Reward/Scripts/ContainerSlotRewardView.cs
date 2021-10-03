using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rewards
{
    internal sealed class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;

        public void SetData(Reward reward, int countDay, bool isSelect)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            _textDays.text = $"Day {countDay}";

            _originalBackground.gameObject.SetActive(isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
