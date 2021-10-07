using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(MapSizeConfig), menuName = "Configs/" + nameof(MapSizeConfig))]
    internal sealed class MapSizeConfig : ScriptableObject
    {
       
        [field: Header("Map Configs:")]
        [field: SerializeField] public EasyMapConfig EasyMapConfig { get; private set; }
        [field: SerializeField] public MediumMapConfig MediumMapConfig { get; private set; }
        [field: SerializeField] public HardMapConfig HardMapConfig { get; private set; }
        [field: SerializeField] public InsaneMapConfig InsaneMapConfig { get; private set; }


        [field: Header("Ground Tiles For Level Floor:")]
        [field: SerializeField] public List<TileBase> GroundTiles { get; private set; }
        [field: SerializeField] public TileBase OldSpawnsTile { get; private set; }


        [field: Header("Borders & Walls:")]
        [field: SerializeField] public TileBase WallsTile { get; private set; }
        [field: SerializeField] public TileBase BordersTile { get; private set; }

        [field: Header("Headquaters:")]
        [field: SerializeField] public List<TileBase> HeadquatersTiles { get; private set; }

        [field: Header("Spawns:")]
        [field: SerializeField] public TileBase PlayerSpawnTiles { get; private set; }
        [field: SerializeField] public TileBase EnemiesSpawnTiles { get; private set; }
    }
}