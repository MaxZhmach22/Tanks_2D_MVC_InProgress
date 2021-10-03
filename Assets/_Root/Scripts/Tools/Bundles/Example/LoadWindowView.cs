using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [SerializeField] private Button _loadAssetButton;

        private void Start()
        {
            _loadAssetButton.onClick.AddListener(LoadAsset);

        }

        private void OnDestroy()
        {
            _loadAssetButton.onClick.RemoveAllListeners();
        }

        private void LoadAsset()
        {
            _loadAssetButton.interactable = false;
            StartCoroutine(DownloadAssetBundle());
        }
    }
}
