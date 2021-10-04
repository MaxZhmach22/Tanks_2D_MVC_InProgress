using UnityEngine;
using Tools;

namespace Tanks
{
    internal sealed class PlayerController : BaseViewController<PlayerView>
    {
        private ResourcePath _resourcePath = new ResourcePath("Prefabs/Player/playerView");

        private readonly PlayerProfile _playerProfile;
        private PlayerView _playerView;

        protected override ResourcePath ResourcePath => _resourcePath;

        public PlayerController(Transform placeForPlayerSpawn, PlayerProfile playerProfile,
                                SubscriptionProperty<MoveDirection> moveDirection,
                                SubscriptionProperty<bool> buttonState)
        {
            _playerProfile = playerProfile;
            _playerView = LoadView(placeForPlayerSpawn);
            _playerView.Init(moveDirection, buttonState);
        }
    }
}