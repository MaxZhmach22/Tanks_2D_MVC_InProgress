using System;
using UnityEngine;

namespace Rewards
{
    [Serializable]
    internal class Reward
    {
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public string RewardName { get; private set; }
        [field: SerializeField] public Sprite IconCurrency { get; private set; }
    }
}