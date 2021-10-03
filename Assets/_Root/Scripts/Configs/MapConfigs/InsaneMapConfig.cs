using UnityEngine;

namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(InsaneMapConfig), menuName = "Configs/MapType/" + nameof(InsaneMapConfig), order = 4)]
    internal sealed class InsaneMapConfig : MapSizeBase
    {
    }
}