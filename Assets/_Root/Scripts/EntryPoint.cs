using UnityEngine;

namespace Tanks
{
    internal sealed class EntryPoint : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _placeForUi;

        [Header("Configs")]
        [SerializeField] private PlayerProfileConfig _playerProfileConfig;
        [SerializeField] private MapSizeConfig _mapSizeConfig;

        private MainController _mainController;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            var playerProfile = new PlayerProfile(_playerProfileConfig); 
            _mainController = new MainController(_placeForUi, playerProfile, _mapSizeConfig, _mainCamera);
        }
    }
}

