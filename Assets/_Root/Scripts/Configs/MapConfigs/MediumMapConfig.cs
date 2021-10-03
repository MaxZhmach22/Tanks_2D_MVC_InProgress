using UnityEngine;

namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(MediumMapConfig), menuName = "Configs/MapType/" + nameof(MediumMapConfig), order = 2)]
    internal sealed class MediumMapConfig : MapSizeBase
    {
    }
}