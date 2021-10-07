using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal class LevelView : BaseView
    {
        [field: SerializeField] public Tilemap BaseTilemap { get; private set; }

        [field: SerializeField] public Tilemap BorderTilemap { get; private set; }
        [field: SerializeField] public Tilemap Headquarters { get; private set; }
        [field: SerializeField] public Tilemap Spawns { get; private set; }
        [field: SerializeField] public Tilemap Walls { get; private set; }
        [field: SerializeField] public Tilemap Water { get; private set; }
        [field: SerializeField] public Tilemap Decorations { get; private set; }

    }
}