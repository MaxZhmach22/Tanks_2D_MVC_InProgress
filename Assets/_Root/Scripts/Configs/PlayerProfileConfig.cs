using UnityEngine;
namespace Tanks
{
    [CreateAssetMenu(fileName = nameof(PlayerProfileConfig),  menuName = "Configs" + nameof(PlayerProfileConfig))]
    internal sealed class PlayerProfileConfig : ScriptableObject
    {
        [field: SerializeField] public TankSettings TankSettings { get; private set; }
        [field: SerializeField] public TankType TankType { get; private set; }
        [field: SerializeField] public GameState GameState { get; private set; }

        [field: SerializeField] public DifficultType DifficultType { get; private set; }

    }
}