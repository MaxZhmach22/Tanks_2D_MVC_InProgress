using UnityEngine;

namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(HardMapConfig), menuName = "Configs/MapType/" + nameof(HardMapConfig), order = 3)]
    internal sealed class HardMapConfig : MapSizeBase
    {
    }
}