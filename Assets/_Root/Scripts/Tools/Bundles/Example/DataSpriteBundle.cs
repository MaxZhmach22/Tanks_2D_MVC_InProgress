using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
internal class DataSpriteBundle
{
   [field: SerializeField] public string NameAssetBundle { get; private set; }
   [field: SerializeField] public Image Image { get; private set; }

}
