using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(MapSizeConfig), menuName = "Configs/" + nameof(MapSizeConfig))]
    internal sealed class MapSizeConfig : ScriptableObject
    {
       
        [field: SerializeField] public EasyMapConfig EasyMapConfig { get; private set; }
        [field: SerializeField] public MediumMapConfig MediumMapConfig { get; private set; }
        [field: SerializeField] public HardMapConfig HardMapConfig { get; private set; }
        [field: SerializeField] public InsaneMapConfig InsaneMapConfig { get; private set; }

        [field: SerializeField] public List<TileBase> GroundTiles { get; private set; }
        [field: SerializeField] public List<TileBase> BorderTiles { get; private set; }

        [field: SerializeField] public List<TileBase> HeadquatersTiles { get; private set; }

        
    }
}