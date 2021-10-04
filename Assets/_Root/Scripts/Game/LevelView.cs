using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    internal class LevelView : BaseView
    {
        [field: SerializeField] public Tilemap BaseTilemap { get; private set; }

        [field: SerializeField] public Tilemap BorderTilemap { get; private set; }
        [field: SerializeField] public Tilemap Headquarters { get; private set; }
        public Tilemap Trees { get; internal set; }
        public Tilemap Water { get; internal set; }
        public Tilemap Bricks { get; internal set; }
    }
}