using UnityEngine;

namespace Tanks
{
    internal class MapSizeBase : ScriptableObject
    {
        [field: SerializeField] public MapSize MapSize { get; protected set; }
    }
}
