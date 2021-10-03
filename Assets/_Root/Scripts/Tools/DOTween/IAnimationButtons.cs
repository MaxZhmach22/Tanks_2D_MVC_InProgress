using DG.Tweening;
using System;
using System.Collections.Generic;
using Tool;
using UnityEngine.UI;

namespace Tanks
{
    internal interface IAnimationButtons
    {
        IReadOnlyDictionary<string, Button> ButtonsDictionary { get; }
        [field: SerializeField] public AnimationButtonType AnimationButtonType { get; }
        [field: SerializeField] public Ease CurveEase { get; }
        [field: SerializeField] public float Duration { get; }
        [field: SerializeField] public float Strength { get; }

        public void PlayAnimation(Button button, float duration, float strength, Action onStart = null, Action onFinish = null);

    }
}